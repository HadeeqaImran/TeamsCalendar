using Newtonsoft.Json.Linq;

namespace TeamsCalendar.Services
{
    public class HomeService
    {
        string credentialsFile = "C:\\Users\\Noor Links\\source\\repos\\TeamsCalendar\\TeamsCalendar\\credentials.json";
        string adminCredentialsFile = "C:\\Users\\Noor Links\\source\\repos\\TeamsCalendar\\TeamsCalendar\\adminCredentialsFile.json";
        public string OauthRedirect()
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            Console.WriteLine(credentials);
            var redirectUrl = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize?" +
                               "&scope=" + credentials["scopes"].ToString() +
                               "&response_type=code" + "&response_mode=query" +
                               "&state=themessydeveloper" +
                               "&redirect_uri=" + credentials["redirect_url"].ToString() +
                               "&client_id=" + credentials["client_id"].ToString();
            return redirectUrl;
        }

        public string AdminOauthRedirect()
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(adminCredentialsFile));

            var redirectUrl = "https://login.microsoftonline.com/common/adminconsent?" +
                               "&state=themessydeveloper" + "&redirect_uri=" + credentials["redirect_url"].ToString() +
                               "&client_id=" + credentials["client_id"].ToString();
            return redirectUrl;
        }
    }
}
