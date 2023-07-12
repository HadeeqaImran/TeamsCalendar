using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TeamsCalendar.Services;

namespace TeamsCalendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _homeService;

        public HomeController(HomeService homeService)
        {
            _homeService = homeService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult OauthRedirect()
        {
            var redirectUrl = _homeService.OauthRedirect();
            return Redirect(redirectUrl);
        }

        public IActionResult AdminOauthRedirect()
        {
            var redirectUrl = _homeService.AdminOauthRedirect();
            return Redirect(redirectUrl);
       
        }
    }
}
