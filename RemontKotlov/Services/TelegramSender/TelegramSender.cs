using RemontKotlov.ViewModels;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace RemontKotlov.Services.TelegramSender
{
    public class TelegramSender : ITelegramSender
    {
        private readonly TelegramBotClient _botClient;
        private readonly string _chatId;
        public TelegramSender(TelegramBotClient botClient)
        {
            _botClient = botClient;
            _chatId = "6617601125";
        }

        public async Task<bool> SendMessage(Zayavka message, CancellationToken cancellationToken)
        {
            try
            {
                var msg = await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: $"<strong>Name:</strong> {message.Name}\n<strong>PhoneNumber:</strong> <a href=\"tel:{message.PhoneNumber}\">{message.PhoneNumber}</a>\n<strong>Description:</strong> {message.Description}\n",
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
