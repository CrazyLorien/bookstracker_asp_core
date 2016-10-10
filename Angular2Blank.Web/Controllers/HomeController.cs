using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Angular2Blank.Web.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
