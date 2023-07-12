using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace TeamsCalendar.Services
{
    public class OauthService
    {
        string credentialsFile = "C:\\Users\\Noor Links\\source\\repos\\TeamsCalendar\\TeamsCalendar\\credentials.json";
        string adminCredentialsFile = "C:\\Users\\Noor Links\\source\\repos\\TeamsCalendar\\TeamsCalendar\\adminCredentialsFile.json";
        string tokensFile = "C:\\Users\\Noor Links\\source\\repos\\TeamsCalendar\\TeamsCalendar\\tokens.json";
        public RestResponse Callback(string code, string state, string error)
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));

            RestClient restClient = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/token");
            RestRequest restRequest = new RestRequest();

            restRequest.AddParameter("client_id", credentials["client_id"].ToString());
            restRequest.AddParameter("scope", credentials["scopes"].ToString());
            restRequest.AddParameter("redirect_uri", credentials["redirect_url"].ToString());
            restRequest.AddParameter("code", code);
            restRequest.AddParameter("grant_type", "authorization_code");
            restRequest.AddParameter("client_secret", credentials["client_secret"].ToString());

            return restClient.Post(restRequest);        
        }

        public RestResponse AdminCallback(string tenant, string state, string admin_consent)
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(adminCredentialsFile));

            RestClient restClient = new RestClient($"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token");
            RestRequest restRequest = new RestRequest();

            restRequest.AddParameter("client_id", credentials["client_id"].ToString());
            restRequest.AddParameter("scope", credentials["scopes"].ToString());
            restRequest.AddParameter("grant_type", "client_credentials");
            restRequest.AddParameter("client_secret", credentials["client_secret"].ToString());

            return restClient.Post(restRequest);
        }

        public RestResponse RefreshToken()
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestClient restClient = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/token");
            RestRequest restRequest = new RestRequest();

            restRequest.AddParameter("client_id", credentials["client_id"].ToString());
            restRequest.AddParameter("scope", credentials["scopes"].ToString());
            restRequest.AddParameter("redirect_uri", credentials["redirect_url"].ToString());
            restRequest.AddParameter("grant_type", "refresh_token");
            restRequest.AddParameter("client_secret", credentials["client_secret"].ToString());
            restRequest.AddParameter("refresh_token", tokens["refresh_token"].ToString());

            return restClient.Post(restRequest);
        }

    }
}
