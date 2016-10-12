using Microsoft.AspNetCore.Mvc;

namespace Angular2Blank.Web.Controllers
{
    [Route("/")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
