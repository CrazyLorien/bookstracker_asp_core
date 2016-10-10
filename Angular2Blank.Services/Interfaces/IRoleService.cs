using System.Threading;
using System.Threading.Tasks;
using Angular2Blank.Services.Dtos;

namespace Angular2Blank.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDto> CreateAsync(RoleDto role);
        Task UpdateAsync(RoleDto role);
        Task DeleteAsync(int roleId);
        Task<RoleDto> FindByIdAsync(string roleId);
        Task<RoleDto> FindByNameAsync(string roleName);
    }
}
