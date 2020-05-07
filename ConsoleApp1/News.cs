using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class News
    {
        
        const string API_KEY = "93912cb53e894cc282f243dbe1f2d118";
        const string API_URL = "http://newsapi.org/v2/top-headlines?" + "country=ru&";
        const string FINAL_URL = API_URL + "apiKey=" + API_KEY;
        private RestClient RC = new RestClient();

        public class NewsData
        {
            public string _quote { get; set; }
        }

        public class Language
        {
            public string _language { get; set; }
        }

        public class Category {
            public string _category { get; set; }
        }

        public class Country
        {
            public string _country { get; set; }
        }

        public String GetNews (String quote)
        {
            var Request = new RestRequest(FINAL_URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<NewsData>(Response.Content);

            var Temp = Data._quote;

            return $"{Temp}";

        }
        
    }
}
