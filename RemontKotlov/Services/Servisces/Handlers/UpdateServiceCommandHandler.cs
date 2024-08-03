using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RemontKotlov.Entities;
using RemontKotlov.Persistance;
using RemontKotlov.Services.Servisces.Commands;
using RemontKotlov.ViewModels;
using System.Diagnostics;

namespace RemontKotlov.Services.Servisces.Handlers
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ResponseModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateServiceCommandHandler(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseModel> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (service == null)
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Service Not Found",
                    StatusCode = 404
                };

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

            service.Picture = "" + fileName;
            service.Price = request.Price;
            service.Description = request.Description;
            service.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                StatusCode = 201,
                Message = "Service Updated"
            };
        }
    }
}
