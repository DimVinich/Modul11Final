using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Modul11Final.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Кол-во букв" , $"count"),
                        InlineKeyboardButton.WithCallbackData($" Сумма цифр" , $"sum")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот может считать</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Либо кол-во букв в ведённой строке.{Environment.NewLine}Либо сумму чисел при формате строки 22 33 44",
                            cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;

                default:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Я не понимаю чего вы от меня хотите", cancellationToken: ct);
                    break;
            }
        }
    }
}
