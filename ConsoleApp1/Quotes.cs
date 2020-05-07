using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Quotes

    {

        public class QuoteData
        {

            public Quote quote { get; set; }
            public Author author { get; set; }
            public string category { get; set; }
        }

        public class Author
        {
            public string author_name { get; set; }
        }

        public class Quote
        {
            public string quote_text { get; set; }
        }

        const string API_URL = "andruxnet-random-famous-quotes.p.rapidapi.com";
        const string API_KEY = "2888e768f5msh7a120b248d0e47cp1a416djsn7a170fe4a63b";
        

        const string FINAL_URL = API_URL + "?count=1&cat=famous";

        private RestClient RC = new RestClient();

        

     /*   public string GetQuote (string _quote)
        {

            var request = new RestRequest(FINAL_URL);
            var url_request = RC.AddDefaultHeader("x-rapidapi-host", "andruxnet-random-famous-quotes.p.rapidapi.com");
            var key_request = RC.AddDefaultHeader("x-rapidapi-key", "2888e768f5msh7a120b248d0e47cp1a416djsn7a170fe4a63b");
            var response = RC.Get(request);
            var Data = JsonConvert.DeserializeObject<QuoteData>(response.Content);

            return $"Вот вам одна очень известная фраза от {Data.author.author_name}\n {Data.quote.quote_text}";
            
        }
        
       */
        
    }
}
