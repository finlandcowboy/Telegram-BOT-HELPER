using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;


namespace ConsoleApp1
{
    class Weather
    {
        public class WeatherData
        {
            public Current current { get; set; }

        }

        public class Current
        {
            public double temp_c { get; set; }
            public Condition condition {get;set;}
        }

        public class Condition
        {
            public string text { get; set; }
        }



        const string API_URL = http://api.apixu.com/v1/current.json;
        const string API_KEY = "3c869fb67a724215a26171051192808&q=Paris";
        const string FINAL_URL = API_URL + "?key=" + API_KEY + "&lang=ru&q=";
        private RestClient RC = new RestClient();
        
        public Weather()
        {

        }

        public String getWeatherInCity(String city)
        {
            var URL = FINAL_URL + city;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<WeatherData>(Response.Content);

            var Temp = (int)Data.current.temp_c;

            return $"В городе {city} сейчас {Data.current.condition.text}, где-то {Temp} градусов"
        }
    }
}
