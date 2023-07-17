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

            modelBuilder.Entity<Document>()
        .HasOne(d => d.SharedFile)
        .WithOne(sf => sf.Document)
        .HasForeignKey<SharedFile>(sf => sf.Id);
        }
    }
}