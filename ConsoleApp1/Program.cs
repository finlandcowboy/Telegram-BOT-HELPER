using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using telegram;

namespace ConsoleApp1
{




    class Program
    {


        static Dictionary<string, string> Questions;
        static public List<Dictionary<string, string>> Quotes;

        static void Main(string[] args)
        {

            var Data = System.IO.File.ReadAllText(@"C:\Users\User\Documents\programming\Telegram-BOT-HELPER\text.json", System.Text.Encoding.UTF8);
            var Quotes_data = System.IO.File.ReadAllText(@"C:\Users\User\Documents\programming\Telegram-BOT-HELPER\quotes.json", System.Text.Encoding.UTF8);
            Questions = JsonConvert.DeserializeObject<Dictionary<string, string>>(Data);
            Quotes = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(Quotes_data);
            


            var API = new TelegramApi();

            while (true)
            {
                var Updates = API.GetUpdates();

                foreach (var update in Updates)
                {
                    var Question = update.message.text;
                    var Answer = AnswerQuestion(Question);
                    API.sendMessage(Answer, update.message.chat.id);
                }

            }
            Console.ReadKey();

        }


        static string AnswerQuestion(string UserQuestion)
        {
            UserQuestion = UserQuestion.ToLower();
            List<string> Answers = new List<string>();

            foreach (var Question in Questions)
            {
                if (UserQuestion.Contains(Question.Key))
                {
                    Answers.Add(Question.Value);
                }
            }


            if (UserQuestion.StartsWith("фраза"))
            {
                Random rnd = new Random();
                int value = rnd.Next() % 5000;
                if (Quotes[value].ContainsKey("quoteText") && Quotes[value].ContainsKey("quoteAuthor"))
                {
                    Answers.Add( Quotes[value]["quoteAuthor"] + "\n" + Quotes[value]["quoteText"]);
                }
            }



            if (Answers.Count == 0)
            {
                Answers.Add("Я тебя не понимаю (");
            }

            if (UserQuestion.Contains("сколько времени"))
            {
                var Time = DateTime.Now.ToString("HH:mm:ss");
                Answers.Add($"Точное время: {Time}");
            }

            if (UserQuestion.Contains("новост"))
            {
                string _news = "";
                var NewsApi = new News();
                var Headline = NewsApi.GetNews(_news);
                Answers.Add(Headline);
            }

            /*      if (UserQuestion.StartsWith("какая погода в городе"))
                  {
                      var words = UserQuestion.Split(' ');
                      var City = words[words.Length - 1];

                      var WeatherApi = new Weather();
                      var Forecast = WeatherApi.getWeatherInCity(City);
                      Answers.Add(Forecast);
                  }*/


            if (UserQuestion.Contains("какой сегодня день"))
            {
                var Date = DateTime.Now.ToString("dd:MM:yy");
                Answers.Add($"Точное время: {Date}");
            }

            return String.Join(", ", Answers);

        }
    }
}
