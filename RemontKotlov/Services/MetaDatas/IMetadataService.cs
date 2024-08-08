using RemontKotlov.Entities;
using RemontKotlov.ViewModels;

namespace RemontKotlov.Services.MetaDatas
{
    public interface IMetadataService
    {
        public Task<ResponseModel> PostMetadata(MetaDataDTO metaData, CancellationToken cancellationToken);
        public Task<IEnumerable<MetaData>> GetMetadata(CancellationToken cancellationToken);
        public Task<MetaData> GetMetadataById(int id, CancellationToken cancellationToken);
        public Task<ResponseModel> RemoveMetadata(int id, CancellationToken cancellationToken);
    }
}
