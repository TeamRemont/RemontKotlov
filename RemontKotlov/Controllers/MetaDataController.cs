using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RemontKotlov.Entities;
using RemontKotlov.Services.MetaDatas;
using RemontKotlov.ViewModels;

namespace RemontKotlov.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private readonly IMetadataService _metadataService;

        public MetaDataController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        [HttpPost]
        public async Task<ResponseModel> Post(MetaDataDTO dto, CancellationToken cancellationToken)
        {
            return await _metadataService.PostMetadata(dto, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<MetaData> GetId(int id, CancellationToken cancellationToken)
        {
            return await _metadataService.GetMetadataById(id, cancellationToken);
        }

        [HttpGet]
        public async Task<IEnumerable<MetaData>> Get(CancellationToken cancellationToken)
        {
            return await _metadataService.GetMetadata(cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> Remove(int id, CancellationToken cancellationToken)
        {
            return await _metadataService.RemoveMetadata(id, cancellationToken);
        }
    }
}
