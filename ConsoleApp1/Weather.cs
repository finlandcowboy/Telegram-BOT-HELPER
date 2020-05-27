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
            public string type { get; set; }
            public string query { get; set; }
            public string language { get; set; }
            public string unit { get; set; }
        }

        public class Location
        {
            public string name { get; set; }
            public string country { get; set; }
            public string region { get; set; }
            public string lat { get; set; }
            public string lon { get; set; }
            public string timezone_id { get; set; }
            public string localtime { get; set; }
            public int localtime_epoch { get; set; }
            public string utc_offset { get; set; }
        }

        public class Current
        {
            public string observation_time { get; set; }
            public int temperature { get; set; }
            public int weather_code { get; set; }
            public IList<string> weather_icons { get; set; }
            public IList<string> weather_descriptions { get; set; }
            public int wind_speed { get; set; }
            public int wind_degree { get; set; }
            public string wind_dir { get; set; }
            public int pressure { get; set; }
            public float precip { get; set; }
            public int humidity { get; set; }
            public int cloudcover { get; set; }
            public int feelslike { get; set; }
            public int uv_index { get; set; }
            public int visibility { get; set; }
            public string is_day { get; set; }
        }

        public class Example
        {
            public WeatherData request { get; set; }
            public Location location { get; set; }
            public Current current { get; set; }
        }



        const string API_URL = "http://api.weatherstack.com/current";
        const string API_KEY = "a256e378e7e3ed8b7b6f2bb382a5368b";
        const string FINAL_URL = API_URL + "?access_key=" + API_KEY + "&query=";
        private RestClient RC = new RestClient();
        
        public Weather()
        {

        }

        public String getWeatherInCity(String city)
        {
            var URL = FINAL_URL + city;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<Example>(Response.Content);
            Console.WriteLine($"{Data.location.country}");
            return $"В городе {Data.location.country}, {Data.location.name} сейчас {Data.current.weather_descriptions[0]}, где-то {Data.current.temperature} градусов\nСекунда в секунду {Data.location.localtime}";   
        }
    }
}
