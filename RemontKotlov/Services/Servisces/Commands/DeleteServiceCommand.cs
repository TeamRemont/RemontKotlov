using MediatR;
using RemontKotlov.ViewModels;

namespace RemontKotlov.Services.Servisces.Commands
{
    public class DeleteServiceCommand : IRequest<ResponseModel>
    {
        public int Id { get; set; }
    }
}
