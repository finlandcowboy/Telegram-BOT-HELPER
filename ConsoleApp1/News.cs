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

        

        public class Source
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Article
        {
            public Source source { get; set; }
            public string author { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public string urlToImage { get; set; }
            public DateTime publishedAt { get; set; }
            public string content { get; set; }
        }

        public class NewsData
        {
            public string status { get; set; }
            public int totalResults { get; set; }
            public IList<Article> articles { get; set; }
        }


        const string API_KEY = "93912cb53e894cc282f243dbe1f2d118";
        const string API_URL = "http://newsapi.org/v2/";
        private RestClient RC = new RestClient();


        public string getNewsHead(string brand)
        {
            string FINAL_URL = API_URL + "top-headlines?country=ru&apiKey=" + API_KEY;
            var Request = new RestRequest(FINAL_URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<NewsData>(Response.Content);

            return $"{Data.articles[0].source.name} в {Data.articles[0].publishedAt}\n{Data.articles[0].description}\n\n" +
                $"{Data.articles[1].source.name} в {Data.articles[1].publishedAt}\n{Data.articles[1].description}\n\n" +
                $"{Data.articles[2].source.name} в {Data.articles[2].publishedAt}\n{Data.articles[2].description}\n\n"; 
        }
        
    }
}
