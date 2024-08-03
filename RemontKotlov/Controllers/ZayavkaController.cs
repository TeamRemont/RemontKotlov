using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RemontKotlov.Services.TelegramSender;
using RemontKotlov.ViewModels;

namespace RemontKotlov.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZayavkaController : ControllerBase
    {
        private readonly ITelegramSender _telegramSender;

        public ZayavkaController(ITelegramSender telegramSender)
        {
            _telegramSender = telegramSender;
        }

        [HttpPost]
        public async Task<bool> SendMessage(Zayavka model, CancellationToken cancellationToken)
        {
            var response = await _telegramSender.SendMessage(model, cancellationToken);

            return response;
        }
    }
}
