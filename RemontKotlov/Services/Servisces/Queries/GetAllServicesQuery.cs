using MediatR;
using RemontKotlov.Entities;

namespace RemontKotlov.Services.Servisces.Queries
{
    public class GetAllServicesQuery : IRequest<IEnumerable<Service>>
    {
    }
}
