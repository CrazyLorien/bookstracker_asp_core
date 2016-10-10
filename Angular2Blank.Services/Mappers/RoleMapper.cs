using Angular2Blank.Data.Entities;
using Angular2Blank.Services.Dtos;

namespace Angular2Blank.Services.Mappers
{
    public static class RoleMapper
    {
        public static RoleDto MapToDto(this Role role)
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public static Role MapToEntity(this RoleDto dto)
        {
            return new Role
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
