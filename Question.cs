using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using DSharpPlus;

namespace Quizbot
{
    public class Question
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";
        public string question;
        public string answer;

        public Question()
        {
            UserCredential credential;
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            Random random = new Random();
            int randomQuestionIndex = random.Next(0, 2);
			// SpreadsheetId is the url ending to yoru spreadsheet.
            string spreadsheetId = "{Place spreadsheet id here}";
            string questionRange = "{Place cell range for questions";
            SpreadsheetsResource.ValuesResource.GetRequest Request =
                    service.Spreadsheets.Values.Get(spreadsheetId, questionRange);
           
            ValueRange Response = Request.Execute();
            IList<IList<Object>> values = Response.Values;
            
            
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("{0}", values.Count);
                question = values[randomQuestionIndex][0].ToString();
                answer = values[randomQuestionIndex][1].ToString();
                Console.WriteLine("Question");
                Console.WriteLine("{0}", question);
                Console.WriteLine("{0}", answer);
            }
            else
            {
                Console.WriteLine("No data found.");
            }

            // Console.Read();
            return;
        }
    }

}
