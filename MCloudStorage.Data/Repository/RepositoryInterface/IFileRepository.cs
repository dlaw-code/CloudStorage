using MCloudStorage.Data.Entities;

namespace MCloudStorage.Data.Repository.RepositoryInterface
{
    public interface IFileRepository
    {
        void Add(Document document);
    }
}
