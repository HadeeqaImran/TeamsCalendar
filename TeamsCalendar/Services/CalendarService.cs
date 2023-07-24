using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using TeamsCalendar.Controllers;
using TeamsCalendar.Models;
using Microsoft.AspNetCore.Mvc;
using TeamsCalendar.Services;

namespace TeamsCalendar.Services
{
    public class CalendarService
    {
        string tokensFile = "C:\\Users\\Noor Links\\source\\repos\\TeamsCalendar\\TeamsCalendar\\tokens.json";

        public RestResponse CreateEvent(CalendarEvent calendarEvent)
        {
            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestClient restClient = new RestClient("https://graph.microsoft.com/v1.0/me/calendar/events");
            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + tokens["access_token"].ToString());
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", JsonConvert.SerializeObject(calendarEvent), ParameterType.RequestBody);

            return restClient.Post(restRequest);
        }

        public RestResponse GetAllEents()
        {
            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestClient restClient = new RestClient("https://graph.microsoft.com/v1.0/me/calendar/events");
            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + tokens["access_token"].ToString());
            restRequest.AddHeader("Prefer", "outlook.timezone=\"Pakistan Standard Time\"");

            return restClient.Get(restRequest);
        }
    }
}
