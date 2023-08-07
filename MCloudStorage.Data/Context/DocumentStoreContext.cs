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
        public DbSet<SharedFile> SharedFiles => Set<SharedFile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-one relationship between Document and SharedFile
            modelBuilder.Entity<SharedFile>()
                .HasOne(sf => sf.SharedDocument)
                .WithOne(d => d.SharedFile)
                .HasForeignKey<SharedFile>(sf => sf.DocumentId);
        }
    }
}