using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_bot___Send_muzik
{
    public partial class Form1 : Form
    {
        TelegramBotClient client;
        string lastmessage = "";

        [Obsolete]
        public Form1()
        {
            InitializeComponent();

            client = new TelegramBotClient("6729845188:AAGuXS1RFTtOOVBx4kIASjfpgUqViJgonBI");

            client.OnMessage += Client_OnMessage;
            client.OnCallbackQuery += Client_OnCallbackQuery;

            client.StartReceiving();
        }
        [Obsolete]
        private async void Client_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            var ChatId = e.CallbackQuery.Message.Chat.Id;
            var Data = e.CallbackQuery.Data;


            string muzik1 = @"E:\muzik\Ibrahim Tatlises - Dallam.mp3",
                muzik2 = @"E:\muzik\michael-jackson-billie-jean.mp3",
                muzik3 = @"E:\muzik\Jarico - Landscape (No Copyright Music).mp3",
                muzik4 = @"E:\muzik\merry christmas.mp3";

            switch (Data)
            {
                case "muzik1":
                await client.SendTextMessageAsync(ChatId, "موزیک در حال ارسال .. 😎");
                await  SendAudio(client, muzik1, ChatId, "این موزیک شماره 1 هست");
                break;
                case "muzik2":
                await client.SendTextMessageAsync(ChatId, "موزیک در حال ارسال .. 😎");
                await SendAudio(client, muzik2, ChatId, "این موزیک شماره 2 هست");
                break;
                case "muzik3":
                await client.SendTextMessageAsync(ChatId, "موزیک در حال ارسال .. 😎");
                await SendAudio(client, muzik3, ChatId, "این موزیک شماره 3 هست");
                break;
                case "muzik4":
                await client.SendTextMessageAsync(ChatId, "موزیک در حال ارسال .. 😎");
                await SendAudio(client, muzik4, ChatId, "این موزیک شماره 4 هست");
                break;
            }
        }

        [Obsolete]
        private void Client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var ChatId = e.Message.Chat.Id;
            var Message = e.Message.Text;

            var keybuttons = new InlineKeyboardButton[][]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("موزیک 1 🎶", "muzik1"),
                      InlineKeyboardButton.WithCallbackData("موزیک 2 🎶", "muzik2")
                },
                  new []
                {
                    InlineKeyboardButton.WithCallbackData("موزیک 3 🎶", "muzik4"),
                      InlineKeyboardButton.WithCallbackData("موزیک 4 🎶", "muzik4")
                }
            };

            InlineKeyboardMarkup markup = new InlineKeyboardMarkup(keybuttons);


            StringBuilder builder = new StringBuilder();

            builder.Append("سلام خوش آمدید دستورات من :");
            builder.AppendLine();
            builder.Append("/muzik = ارسال موزیک");

            if (Message != lastmessage && Message != "/muzik")
            {
                client.SendTextMessageAsync(ChatId, builder.ToString());
                lastmessage = Message;
            }
            else if (Message.Equals("/muzik"))
            {
                client.SendTextMessageAsync(ChatId, "موزیک های ربات", replyMarkup: markup);
            }

        }
        private static async Task SendAudio (TelegramBotClient bot, string path, long ChatId, string Caption)
        {
            try
            {
                using(var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var Ip = new InputOnlineFile(filestream);


                    await bot.SendAudioAsync(ChatId, Ip, Caption);
                }
            }catch (Exception e)
            {

            }
        }
    }
}
