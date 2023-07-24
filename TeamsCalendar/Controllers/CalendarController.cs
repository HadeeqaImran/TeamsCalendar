using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using TeamsCalendar.Models;
using TeamsCalendar.Services;

namespace TeamsCalendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : Controller
    {
        private readonly CalendarService _calendar;
        string tokensFile = "C:\\Users\\Noor Links\\source\\repos\\TeamsCalendar\\TeamsCalendar\\tokens.json";

        public CalendarController(CalendarService calendar)
        {
            _calendar = calendar;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("event")]
        public ActionResult CreateEvent(CalendarEvent calendarEvent)
        {
            var response = _calendar.CreateEvent(calendarEvent);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                System.IO.File.WriteAllText(tokensFile, response.Content);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Error");
        }

        [HttpGet("events")]
        public ActionResult GetAllEvents(CalendarEvent calendarEvent)
        {
            var response = _calendar.GetAllEents();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject eventsList = JObject.Parse(response.Content);
                var calendarEvents = eventsList["value"].ToObject<IEnumerable<CalendarEvent>>();
                return Ok(calendarEvents);
            }
            return RedirectToAction("Error");
        }
    }
}
