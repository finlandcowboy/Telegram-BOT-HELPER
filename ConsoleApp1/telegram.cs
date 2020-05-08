using RestSharp;
using System;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace telegram



{
    public class TelegramApi
    {


        

        

        Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new Telegram.Bot.Types.ReplyMarkups.KeyboardButton("хуй"));




        public class Message
        {
            public Chat chat { get; set; }
            public String text { get; set; }
        }

        public class Chat
        {
            public int id { get; set; }
            public String first_name { get; set; }
        }

        public class Update
        {
            public int update_id { get; set; }
            public Message message { get; set; }
        }

        public class ApiResult
        {
            public Update[] result { get; set; }

        }
        const String API_KEY = "1088789624:AAH7fPuPwkFIdst-uu07Nl4X2XSYXWFX6D4";
        const String API_URL = "https://api.telegram.org/bot" + API_KEY + "/";

        

        RestClient RC = new RestClient();

        private int last_update_id = 0;

        public TelegramApi()
        {
            

        }

        public void sendMessage(string text, int chat_id, ReplyKeyboardMarkup reply)
        {
            sendApiRequest("sendMessage", $"chat_id={chat_id}&text={text}&reply_markup={reply}");
        }

        public Update[] GetUpdates()
        {
            var json = sendApiRequest("getUpdates","offset="+ last_update_id);
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(json);
            if (apiResult != null)
            {
                foreach (var update in apiResult.result)
                {
                  Console.WriteLine($"Получен апдейт {update.update_id}," + $"сообщение от {update.message.chat.first_name}," + $"текст: {update.message.text}");
                  last_update_id = update.update_id + 1;
                }

                return apiResult.result;
            }
            else return null;
        }   



        public string sendApiRequest(string ApiMethod, string Params)
        {
            var URL = API_URL + ApiMethod + "?" + Params;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);

            

            return Response.Content;
        }
    }
}