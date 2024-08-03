using MediatR;
using Microsoft.EntityFrameworkCore;
using RemontKotlov.Entities;
using RemontKotlov.Persistance;
using RemontKotlov.Services.Servisces.Queries;

namespace RemontKotlov.Services.Servisces.Handlers
{
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<Service>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllServicesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Services.ToListAsync(cancellationToken);
        }
    }
}
