using MCloudStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MCloudStorage.API.Data
{
    public class DocumentStoreContext : DbContext
    {
        public DocumentStoreContext(DbContextOptions<DocumentStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents => Set<Document>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}