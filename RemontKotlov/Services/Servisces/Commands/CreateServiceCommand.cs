using RemontKotlov.ViewModels;
using MediatR;

namespace RemontKotlov.Services.Servisces.Commands
{
    public class CreateServiceCommand : IRequest<ResponseModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public long Price { get; set; }
    }
}
