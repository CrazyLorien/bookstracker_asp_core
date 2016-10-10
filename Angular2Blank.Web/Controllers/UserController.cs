using System.Threading.Tasks;
using Angular2Blank.Services.Dtos;
using Angular2Blank.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Angular2Blank.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController: BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<UserDto> Get(int id)
        {
            return _userService.FindByIdAsync(id);
        }

        [HttpGet]
        [Route("userinfo")]
        public Task<UserDto> GetUserInfo()
        {
            return _userService.FindByIdAsync(CurrentUserId);
        }
    }
}
