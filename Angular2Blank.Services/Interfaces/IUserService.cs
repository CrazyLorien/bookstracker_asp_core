using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Angular2Blank.Services.Dtos;

namespace Angular2Blank.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(UserDto user);
        Task UpdateAsync(UserDto user);
        Task DeleteAsync(UserDto user);
        Task<UserDto> FindByIdAsync(int userId);
        Task<UserDto> FindByNameAsync(string userName);
        Task<UserDto> FindByEmailAsync(string email);
        Task AddToRoleAsync(UserDto user, string roleName);
        Task RemoveFromRoleAsync(UserDto user, string roleName);
        Task<IList<string>> GetRolesAsync(UserDto user);
        Task<bool> IsInRoleAsync(UserDto user, string roleName);
        Task<IList<UserDto>> GetUsersInRoleAsync(string roleName);
    }
}
