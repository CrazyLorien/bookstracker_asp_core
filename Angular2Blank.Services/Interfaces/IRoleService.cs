using System.Threading;
using System.Threading.Tasks;
using Angular2Blank.Services.Dtos;

namespace Angular2Blank.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDto> CreateAsync(RoleDto role, CancellationToken cancellationToken);
        Task UpdateAsync(RoleDto role, CancellationToken cancellationToken);
        Task DeleteAsync(RoleDto role, CancellationToken cancellationToken);
        Task<RoleDto> FindByIdAsync(string roleId, CancellationToken cancellationToken);
        Task<RoleDto> FindByNameAsync(string roleName, CancellationToken cancellationToken);
    }
}
