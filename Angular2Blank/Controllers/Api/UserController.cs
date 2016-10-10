using System.Threading;
using System.Threading.Tasks;
using Angular2Blank.Services.Dtos;
using Angular2Blank.Services.Interfaces;
using Microsoft.AspNet.Mvc;

namespace Angular2Blank.Controllers.Api
{
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public Task<UserDto> Get(int id)
        {
            return _userService.FindByIdAsync(id.ToString(), new CancellationToken());
        }
    }
}
