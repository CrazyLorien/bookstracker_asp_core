using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Angular2Blank.Web.Controllers
{
    [Authorize]
    public abstract class BaseController: Controller
    {
        protected int CurrentUserId
        {
            get
            {
                int result = default(int);

                if (User == null)
                    return result;

                var nameIdClaims = User.FindFirst(ClaimTypes.NameIdentifier);
                if (nameIdClaims == null)
                    return result;

                int.TryParse(nameIdClaims.Value, out result);

                return result;
            }
        }
    }
}
