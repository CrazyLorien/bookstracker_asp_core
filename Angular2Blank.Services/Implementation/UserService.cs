﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Angular2Blank.Data.Entities;
using Angular2Blank.Data.Repository;
using Angular2Blank.Services.Dtos;
using Angular2Blank.Services.Interfaces;
using Angular2Blank.Services.Mappers;
using Angular2Blank.Data.Extensions;

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

        public async Task<UserDto> CreateAsync(UserDto user, string passwordHash)
        {
            var entity = user.MapToEntity();
            entity.PasswordHash = passwordHash;

            await Repository.AddAsync(entity);

            return entity.MapToDto();
        }

        public async Task UpdateAsync(UserDto user)
        {
            var entity = await Repository.GetById(user.Id);
            entity.Email = user.Email;
            entity.UserName = user.UserName;

            await Repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int userId)
        {
            var entity = await Repository.GetById(userId);
            await Repository.DeleteAsync(entity);
        }

        public async Task<UserDto> FindByIdAsync(int userId)
        {
            var user = await Repository.GetById(userId);
            return user.MapToDto();
        }

        public async Task<UserDto> FindByNameAsync(string userName)
        {
            var user = await Repository.GetQuery()
                .FirstOrDefaultAsync(x => x.UserName == userName);

            return user.MapToDto();
        }

        public Task<string> GetPasswordHashAsync(string userName)
        {
            var passwordHash = Repository.GetQuery()
                .Where(x => x.UserName == userName)
                .Select(x => x.PasswordHash)
                .FirstOrDefaultAsync();

            return passwordHash;
        }

        public async Task<UserDto> FindByEmailAsync(string email)
        {
            var user = await Repository.GetQuery()
                    .FirstOrDefaultAsync(x => x.Email == email);

            return user.MapToDto();
        }

        public async Task AddToRoleAsync(int userId, string roleName)
        {
            var isInRole = await IsInRoleAsync(userId, roleName);

            if (isInRole)
                return;

            var role = await GetRoleByName(roleName);

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = role.Id
            };

            await _userRoleRepository.AddAsync(userRole);
        }

        public async Task RemoveFromRoleAsync(int userId, string roleName)
        {
            var userRole = await _userRoleRepository.GetQuery()
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.Role.Name == roleName);

            if (userRole == null)
                return;

            await _userRoleRepository.DeleteAsync(userRole);
        }

        public Task<IList<string>> GetRolesAsync(int userId)
        {
            var roles = _userRoleRepository.GetQuery()
                    .Where(x => x.UserId == userId)
                    .Select(x => x.Role.Name)
                    .ToList();

            return Task.FromResult<IList<string>>(roles);
        }

        private async Task<Role> GetRoleByName(string roleName)
        {
            var role = await _roleRepository.GetQuery()
                    .FirstOrDefaultAsync(x => x.Name == roleName);

            return role;
        }

        public async Task<bool> IsInRoleAsync(int userId, string roleName)
        {
            var role = await GetRoleByName(roleName);

            var hasRole = _userRoleRepository.GetQuery()
                .Any(x => x.UserId == userId && x.RoleId == role.Id);

            return hasRole;
        }

        public Task<IList<UserDto>> GetUsersInRoleAsync(string roleName)
        {
            var usersDtos = _userRoleRepository.GetQuery()
                    .Where(x => x.Role.Name == roleName)
                    .Select(x => x.User)
                    .ToList()
                    .Select(x => x.MapToDto())
                    .ToList();

            return Task.FromResult<IList<UserDto>>(usersDtos);
        }
    }
}
