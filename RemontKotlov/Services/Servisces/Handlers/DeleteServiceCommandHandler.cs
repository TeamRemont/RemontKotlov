using MediatR;
using Microsoft.EntityFrameworkCore;
using RemontKotlov.Persistance;
using RemontKotlov.Services.Servisces.Commands;
using RemontKotlov.ViewModels;

namespace RemontKotlov.Services.Servisces.Handlers
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, ResponseModel>
    {
        private readonly ApplicationDbContext _context;

        public DeleteServiceCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (service == null)
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Service Not Found",
                    StatusCode = 404
                };

            _context.Services.Remove(service);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Service Deleted",
                StatusCode = 200
            };
        }
    }
}
