using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using TeamsCalendar.Services;

namespace TeamsCalendar.Controllers
{
    public class OauthController : Controller
    {
        private readonly OauthService _oauthService;
        string tokensFile = "C:\\Users\\Noor Links\\source\\repos\\TeamsCalendar\\TeamsCalendar\\tokens.json";
        public OauthController(OauthService oauthService)
        {
            _oauthService = oauthService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Callback(string code, string state, string error)
        {
            if(!string.IsNullOrWhiteSpace(code))
            {
                var response = _oauthService.Callback(code, state, error);
                if (response.StatusCode == System.Net.HttpStatusCode.OK) 
                {
                    System.IO.File.WriteAllText(tokensFile, response.Content);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Error");
        }

        public ActionResult AdminCallback(string tenant, string state, string admin_consent)
        {
            if (!string.IsNullOrWhiteSpace(tenant))
            {
                var response = _oauthService.AdminCallback(tenant, state, admin_consent);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    System.IO.File.WriteAllText(tokensFile, response.Content);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RefreshToken()
        {
            var response = _oauthService.RefreshToken();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                System.IO.File.WriteAllText(tokensFile, response.Content);
                return RedirectToAction("Index", "Home");
            }
        
            return RedirectToAction("Error");

    }
    }
}
