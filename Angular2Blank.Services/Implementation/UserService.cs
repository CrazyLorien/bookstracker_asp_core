using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Angular2Blank.Data.Entities;
using Angular2Blank.Data.Repository;
using Angular2Blank.Services.Dtos;
using Angular2Blank.Services.Interfaces;
using Angular2Blank.Services.Mappers;
using Microsoft.Data.Entity;

namespace Angular2Blank.Services.Implementation
{
    public class UserService: ServiceBase<User>, IUserService
    {
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Role> _roleRepository;

        public UserService(IRepository<User> userRepository, 
            IRepository<UserRole> userRoleRepository,
            IRepository<Role> roleRepository): base(userRepository)
        {
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public Task<UserDto> CreateAsync(UserDto user, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var entity = user.MapToEntity();
                await Repository.AddAsync(entity);

                return entity.MapToDto();
            }, cancellationToken);
        }

        public Task UpdateAsync(UserDto user, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var entity = user.MapToEntity();
                await Repository.UpdateAsync(entity);
            }, cancellationToken);
        }

        public Task DeleteAsync(UserDto user, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var entity = user.MapToEntity();
                await Repository.DeleteAsync(entity);
            }, cancellationToken);
        }

        public Task<UserDto> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.Run(async () => 
            {
                var user = await Repository.GetById(int.Parse(userId));
                return user.MapToDto();
            }, cancellationToken);
        }

        public Task<UserDto> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var user = await Repository.GetQuery()
                    .FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken: cancellationToken);

                return user.MapToDto();
            }, cancellationToken);
        }

        public Task<UserDto> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var user = await Repository.GetQuery()
                    .FirstOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);

                return user.MapToDto();
            }, cancellationToken);
        }

        public Task AddToRoleAsync(UserDto user, string roleName, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var isInRole = await IsInRoleAsync(user, roleName, cancellationToken);

                if (isInRole)
                    return;

                var role =  await GetRoleByName(roleName);

                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };

                await _userRoleRepository.AddAsync(userRole);

            }, cancellationToken);
        }

        public Task RemoveFromRoleAsync(UserDto user, string roleName, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var userRole = await _userRoleRepository.GetQuery()
                    .FirstOrDefaultAsync(x => x.UserId == user.Id && x.Role.Name == roleName, cancellationToken: cancellationToken);

                if (userRole == null)
                    return;

                await _userRoleRepository.DeleteAsync(userRole);
            }, cancellationToken);
        }

        public Task<IList<string>> GetRolesAsync(UserDto user, CancellationToken cancellationToken)
        {
            return Task.Run<IList<string>>(() =>
            {
                var roles = _userRoleRepository.GetQuery()
                    .Where(x => x.UserId == user.Id)
                    .Select(x => x.Role.Name)
                    .ToList();

                return roles;
            }, cancellationToken);
        }

        private Task<Role> GetRoleByName(string roleName)
        {
            return Task.Run(async () =>
            {
                var role = await _roleRepository.GetQuery()
                    .FirstOrDefaultAsync(x => x.Name == roleName);

                return role;
            });
        }

        public Task<bool> IsInRoleAsync(UserDto user, string roleName, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var role = await GetRoleByName(roleName);

                var hasRole = _userRoleRepository.GetQuery()
                    .Any(x => x.UserId == user.Id && x.RoleId == role.Id);

                return hasRole;

            }, cancellationToken);
        }

        public Task<IList<UserDto>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return Task.Run<IList<UserDto>>(() =>
            {
                var usersDtos = _userRoleRepository.GetQuery()
                    .Where(x => x.Role.Name == roleName)
                    .Select(x => x.User)
                    .ToList()
                    .Select(x => x.MapToDto())
                    .ToList();

                return usersDtos;
            }, cancellationToken);
        }
    }
}
