using MediatR;
using RemontKotlov.ViewModels;

namespace RemontKotlov.Services.Servisces.Commands
{
    public class UpdateServiceCommand : IRequest<ResponseModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public long Price { get; set; }
    }
}
