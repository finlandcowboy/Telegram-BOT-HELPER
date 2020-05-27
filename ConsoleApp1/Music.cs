using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConsoleApp1
{
    class Music
    {
        public partial class MusicData
        {
            public Info Info { get; set; }
            public Dictionary<string, Content> Content { get; set; }
        }

        public partial class Content
        {
            public long Rank { get; set; }
            public string Title { get; set; }
            public string Artist { get; set; }
            public long? WeeksAtNo1 { get; set; }
            public long LastWeek { get; set; }
            public long PeakPosition { get; set; }
            public long WeeksOnChart { get; set; }
            public string Detail { get; set; }
        }

        public partial class Info
        {
            public string Category { get; set; }
            public string Chart { get; set; }
            public DateTimeOffset Date { get; set; }
            public string Source { get; set; }
        }

        const string API_URL = "https://billboard-api2.p.rapidapi.com/hot-100?date=";
        const string API_KEY = "2888e768f5msh7a120b248d0e47cp1a416djsn7a170fe4a63b";

        public string getHOT(string a)
        {
            string Date = DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd");
            


            string FINAL_URL = API_URL + Date + "&range=1-10";            
            var client = new RestClient(FINAL_URL);
        


            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "billboard-api2.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "2888e768f5msh7a120b248d0e47cp1a416djsn7a170fe4a63b");
            IRestResponse response = client.Execute(request);


            var Data = JsonConvert.DeserializeObject<MusicData>(response.Content);


           

            string temp = $"Топ-5 чарта Billboard на {Date}\n\n";

            for(int i = 1; i < 6; i++)
            {
                temp += $"{i}.{Data.Content[$"{i}"].Artist} - {Data.Content[$"{i}"].Title}\n";
            }
            temp = temp.Replace("&", "featuring");
            
            return temp;
        }






    }
}
