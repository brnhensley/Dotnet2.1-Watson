using System;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.PersonalityInsights.v3.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Watson
{
    public class WatsonService
    {
        public WatsonService()
        {
            string apikey = "{apikey}";
            string url = "{serviceUrl}";
            string versionDate = "{versionDate}";

            void Main(string[] args)
            {
                WatsonService example = new WatsonService();

                example.Profile();
                example.ProfileAsCsv();

                Console.WriteLine("Examples complete. Press any key to close the application.");
                Console.ReadKey();
            }

            #region Profile
            void Profile()
            {
                IamAuthenticator authenticator = new IamAuthenticator(
                    apikey: "{apikey}");

                PersonalityInsightsService service = new PersonalityInsightsService("2017-10-13", authenticator);
                service.SetServiceUrl("{serviceUrl}");

                Content content = null;
                content = JsonConvert.DeserializeObject<Content>(File.ReadAllText("profile.json"));

                var result = service.Profile(
                    content: content,
                    contentType: "application/json",
                    rawScores: true,
                    consumptionPreferences: true
                    );

                Console.WriteLine(result.Response);
            }

            void ProfileAsCsv()
            {
                IamAuthenticator authenticator = new IamAuthenticator(
                    apikey: "{apikey}");

                PersonalityInsightsService service = new PersonalityInsightsService("2017-10-13", authenticator);
                service.SetServiceUrl("{serviceUrl}");

                Content content = null;
                content = JsonConvert.DeserializeObject<Content>(File.ReadAllText("profile.json"));

                var result = service.ProfileAsCsv(
                    content: content,
                    contentType: "application/json",
                    consumptionPreferences: true,
                    rawScores: true,
                    csvHeaders: true
                    );

                using (FileStream fs = File.Create("output.csv"))
                {
                    result.Result.WriteTo(fs);
                    fs.Close();
                    result.Result.Close();
                }
            }
            #endregion
        }
    }
}