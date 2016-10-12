using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Angular2Blank.Services.Dtos;

namespace Angular2Blank.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(UserDto user, string passwordHash);
        Task UpdateAsync(UserDto user);
        Task DeleteAsync(int userId);
        Task<UserDto> FindByIdAsync(int userId);
        Task<UserDto> FindByNameAsync(string userName);
        Task<string> GetPasswordHashAsync(string userName);
        Task<UserDto> FindByEmailAsync(string email);
        Task AddToRoleAsync(int user, string roleName);
        Task RemoveFromRoleAsync(int user, string roleName);
        Task<IList<string>> GetRolesAsync(int user);
        Task<bool> IsInRoleAsync(int user, string roleName);
        Task<IList<UserDto>> GetUsersInRoleAsync(string roleName);
    }
}
