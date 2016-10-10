using System.Threading.Tasks;
using Angular2Blank.Data.Entities;
using Angular2Blank.Data.Repository;
using Angular2Blank.Services.Dtos;
using Angular2Blank.Services.Interfaces;
using Angular2Blank.Services.Mappers;
using Angular2Blank.Data.Extensions;

namespace Angular2Blank.Services.Implementation
{
    public class RoleService : ServiceBase<Role>, IRoleService
    {
        public RoleService(IRepository<Role> repository) : base(repository)
        {
        }

        public async Task<RoleDto> CreateAsync(RoleDto role)
        {
            var entity = role.MapToEntity();
            await Repository.AddAsync(entity);
            return entity.MapToDto();
        }

        public async Task UpdateAsync(RoleDto role)
        {
            var entity = await Repository.GetById(role.Id);
            entity.Name = role.Name;

            await Repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int roleId)
        {
            var entity = await Repository.GetById(roleId);
            await Repository.DeleteAsync(entity);
        }

        public async Task<RoleDto> FindByIdAsync(string roleId)
        {
            var role = await Repository.GetById(int.Parse(roleId));
            return role.MapToDto();
        }

        public async Task<RoleDto> FindByNameAsync(string roleName)
        {
            var role = await Repository.GetQuery()
                .FirstOrDefaultAsync(x => x.Name == roleName);

            return role.MapToDto();
        }
    }
}
