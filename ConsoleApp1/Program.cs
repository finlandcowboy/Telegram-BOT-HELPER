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
        static string [] Playlists;

        static void Main(string[] args)
        {



            Playlists = System.IO.File.ReadAllLines(@"C:\Users\User\Documents\programming\Telegram-BOT-HELPER\playlists.txt");
            
            var Quotes_data = System.IO.File.ReadAllText(@"C:\Users\User\Documents\programming\Telegram-BOT-HELPER\quotes.json", System.Text.Encoding.UTF8);
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

            
            if (UserQuestion.Contains("музыка") || UserQuestion.Contains("плейлист"))
            {
                Random rnd = new Random();
                int value = rnd.Next() % 59;
                Answers.Add(Playlists[value]);
            }

            if (UserQuestion.StartsWith("фраза"))
            {
                Random rnd = new Random();
                int value = rnd.Next() % 5000;
                if (Quotes[value].ContainsKey("quoteText") && Quotes[value].ContainsKey("quoteAuthor"))
                {
                    Answers.Add("Однажды один великий человек (" + Quotes[value]["quoteAuthor"]+ ") сказал очень умную вещь!\n\n" + Quotes[value]["quoteText"]);
                }
            }


            if(UserQuestion.Contains("топ чарты"))
            {
                string a = "";
                var MusicApi = new Music();
                var ChartTop = MusicApi.getHOT(a);
                Answers.Add(ChartTop);

            }
            

            if (UserQuestion.Contains("сколько времени"))
            {
                var Time = DateTime.Now.ToString("HH:mm:ss");
                Answers.Add($"Точное время: {Time}");
            }

            if (UserQuestion.StartsWith("новост"))
            {
                var NewsApi = new News();
                var Headlines = NewsApi.getNewsHead("а");
                Answers.Add(Headlines);
            }

            if (UserQuestion.StartsWith("какая погода в городе"))
            {
                var words = UserQuestion.Split(' ');
                var City = words[words.Length - 1];

                var WeatherApi = new Weather();
                var Forecast = WeatherApi.getWeatherInCity(City);
                Answers.Add(Forecast);
            }

           

            



            if (UserQuestion.Contains("какой сегодня день"))
            {
                var Date = DateTime.Now.ToString("dd:MM:yy");
                Answers.Add($"Точное время: {Date}");
            }

            if (Answers.Count == 0)
            {
                Answers.Add("Я тебя не понимаю (");
            }


            return String.Join(", ", Answers);     

            





        }
    }
}
