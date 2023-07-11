using MCloudStorage.API.Data;
using MCloudStorage.Data.Entities;
using MCloudStorage.Data.Repository.RepositoryInterface;

namespace MCloudStorage.Data.Repository.Implementation
{
    public class  FileRepository : IFileRepository
    {
        private readonly DocumentStoreContext _dbContext;

        public FileRepository(DocumentStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Document document)
        {
            _dbContext.Documents.Add(document); // Add the Document entity to the context
            _dbContext.SaveChanges(); // Save the changes to the database
        }
    }
}