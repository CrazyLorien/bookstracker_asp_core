using System.Collections.Generic;

namespace Angular2Blank.Data.Entities
{
    public class User: BaseEntity
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
