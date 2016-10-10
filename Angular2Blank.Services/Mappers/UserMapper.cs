using Angular2Blank.Data.Entities;
using Angular2Blank.Services.Dtos;

namespace Angular2Blank.Services.Mappers
{
    public static class UserMapper
    {
        public static UserDto MapToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                UserName = user.UserName
            };
        }

        public static User MapToEntity(this UserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                UserName = dto.UserName
            };
        }
    }
}
