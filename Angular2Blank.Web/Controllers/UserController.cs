using System.Threading.Tasks;
using Angular2Blank.Services.Dtos;
using Angular2Blank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Angular2Blank.Services.Providers;
using Microsoft.AspNetCore.Authorization;

namespace Angular2Blank.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController: BaseController
    {
        private readonly IUserService _userService;
        private readonly IPasswordProvider _passProvider;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IPasswordProvider provider, IRoleService roleservice)
        {
            _userService = userService;
            _passProvider = provider;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<UserDto> Get(int id)
        {
            return _userService.FindByIdAsync(id);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("userinfo")]
        public Task<UserDto> GetUserInfo()
        {
            return _userService.FindByIdAsync(CurrentUserId);
        }

        [Route("initialize")]
        public async Task Initialize()
        {
             await _roleService.CreateAsync(new RoleDto() { Name = "user" });
             await _userService.CreateAsync(new UserDto() { UserName = "user1", Email = "user1@start.com" }, _passProvider.GetHash("blackcat"));
        }
    }
}
