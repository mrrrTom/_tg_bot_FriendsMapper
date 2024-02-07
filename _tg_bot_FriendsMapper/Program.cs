using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;

namespace _tg_bot_FriendsMapper
{
    internal class Program
    {
        //ToDo remove from static
        //написать нормалььный hhtpclient с ретраем, таймаутом и блекджеком
        static ITelegramBotClient botClient = new TelegramBotClient("6906252266:AAFL6PY5q6uhe_eniAhOLlmszCoHU6rQrSc");

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("tg_bot " + botClient.GetMeAsync().Result.FirstName  + " started" );

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };

            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.ReadLine();
        }
    }
}
