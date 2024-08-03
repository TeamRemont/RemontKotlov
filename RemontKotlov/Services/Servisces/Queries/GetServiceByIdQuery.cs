using MediatR;
using RemontKotlov.Entities;

namespace RemontKotlov.Services.Servisces.Queries
{
    public class GetServiceByIdQuery : IRequest<Service>
    {
        public int Id { get; set; }
    }
}
