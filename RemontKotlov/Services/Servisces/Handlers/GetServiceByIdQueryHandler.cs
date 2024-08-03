using MediatR;
using Microsoft.EntityFrameworkCore;
using RemontKotlov.Entities;
using RemontKotlov.Persistance;
using RemontKotlov.Services.Servisces.Queries;

namespace RemontKotlov.Services.Servisces.Handlers
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, Service>
    {
        private readonly ApplicationDbContext _context;

        public GetServiceByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Service> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Services.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
                throw new Exception("Not Found");
        }
    }
}
