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
    public class RoleService : ServiceBase<Role>, IRoleService
    {
        public RoleService(IRepository<Role> repository) : base(repository)
        {
        }

        public Task<RoleDto> CreateAsync(RoleDto role, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var entity = role.MapToEntity();
                await Repository.AddAsync(entity);

                return entity.MapToDto();
            }, cancellationToken);
        }

        public Task UpdateAsync(RoleDto role, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var entity = role.MapToEntity();
                await Repository.UpdateAsync(entity);
            }, cancellationToken);
        }

        public Task DeleteAsync(RoleDto role, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var entity = await Repository.GetById(role.Id);
                await Repository.DeleteAsync(entity);
            }, cancellationToken);
        }

        public Task<RoleDto> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var role = await Repository.GetById(int.Parse(roleId));

                return role.MapToDto();
            }, cancellationToken);
        }

        public Task<RoleDto> FindByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var role = await Repository.GetQuery()
                    .FirstOrDefaultAsync(x => x.Name == roleName, cancellationToken: cancellationToken);

                return role.MapToDto();
            }, cancellationToken);
        }
    }
}
