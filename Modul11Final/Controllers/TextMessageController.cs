using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Modul11Final.Services;

namespace Modul11Final.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        private readonly IStringServices _stringServices;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, IStringServices stringServices)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _stringServices = stringServices;
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

                    switch (_memoryStorage.GetSession(message.Chat.Id).OperationType)
                    {
                        case "sum":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, _stringServices.CountSum(message.Text), cancellationToken: ct);
                            break;

                        default:
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, _stringServices.CountString(message.Text), cancellationToken: ct);
                            break;
                    }
                    break;
            }
        }
    }
}
