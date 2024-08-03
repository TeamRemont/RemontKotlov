using RemontKotlov.ViewModels;

namespace RemontKotlov.Services.TelegramSender
{
    public interface ITelegramSender
    {
        Task<bool> SendMessage(Zayavka message, CancellationToken cancellationToken);
    }
}
