using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Angular2Blank.Services.Dtos;

namespace Angular2Blank.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(UserDto user, CancellationToken cancellationToken);
        Task UpdateAsync(UserDto user, CancellationToken cancellationToken);
        Task DeleteAsync(UserDto user, CancellationToken cancellationToken);
        Task<UserDto> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<UserDto> FindByNameAsync(string userName, CancellationToken cancellationToken);
        Task<UserDto> FindByEmailAsync(string email, CancellationToken cancellationToken);
        Task AddToRoleAsync(UserDto user, string roleName, CancellationToken cancellationToken);
        Task RemoveFromRoleAsync(UserDto user, string roleName, CancellationToken cancellationToken);
        Task<IList<string>> GetRolesAsync(UserDto user, CancellationToken cancellationToken);
        Task<bool> IsInRoleAsync(UserDto user, string roleName, CancellationToken cancellationToken);
        Task<IList<UserDto>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken);
    }
}
