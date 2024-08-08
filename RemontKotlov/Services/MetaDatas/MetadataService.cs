using Microsoft.EntityFrameworkCore;
using RemontKotlov.Entities;
using RemontKotlov.Persistance;
using RemontKotlov.ViewModels;
using System.Diagnostics;

namespace RemontKotlov.Services.MetaDatas
{
    public class MetadataService : IMetadataService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MetadataService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<MetaData>> GetMetadata(CancellationToken cancellationToken)
        {
            return await _context.Videos.ToListAsync(cancellationToken);
        }

        public async Task<MetaData> GetMetadataById(int id, CancellationToken cancellationToken)
        {
            return await _context.Videos.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new Exception();
        }

        public async Task<ResponseModel> PostMetadata(MetaDataDTO metaData, CancellationToken cancellationToken)
        {
            var file = metaData.Video;
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Videos");
            string fileName = "";

            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Debug.WriteLine("Directory created successfully.");
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Videos", fileName);
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

            var meta = new MetaData()
            {
                Path = "/Videos/" + fileName,
            };

            await _context.Videos.AddAsync(meta, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                Message = "Video Created",
                StatusCode = 201,
                IsSuccess = true
            };
        }

        public async Task<ResponseModel> RemoveMetadata(int id, CancellationToken cancellationToken)
        {
            var metaData = await _context.Videos.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (metaData == null)
            {
                return new ResponseModel()
                {
                    Message = "Not found",
                    StatusCode = 404,
                    IsSuccess = false
                };
            }

            _context.Videos.Remove(metaData);
            await _context.SaveChangesAsync(cancellationToken);
            return new ResponseModel()
            {
                Message = "Deleted",
                StatusCode = 200,
                IsSuccess = true
            };
        }
    }
}
