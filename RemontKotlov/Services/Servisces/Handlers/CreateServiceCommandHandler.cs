using MediatR;
using Microsoft.AspNetCore.Hosting;
using RemontKotlov.Entities;
using RemontKotlov.Persistance;
using RemontKotlov.Services.Servisces.Commands;
using RemontKotlov.ViewModels;
using System.Diagnostics;

namespace RemontKotlov.Services.Servisces.Handlers
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ResponseModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateServiceCommandHandler(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseModel> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var file = request.Picture;
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "ServiceFiles");
            string fileName = "";

            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Debug.WriteLine("Directory created successfully.");
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "ServiceFiles", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Message = ex.Message,
                    StatusCode = 500,
                    IsSuccess = false
                };
            }

            var service = new Service()
            {
                Name = request.Name,
                Description = request.Description,
                Picture = "/ServiceFiles/" + fileName,
                Price = request.Price
            };

            await _context.Services.AddAsync(service, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                StatusCode = 201,
                Message = "Service Created"
            };
        }
    }
}
