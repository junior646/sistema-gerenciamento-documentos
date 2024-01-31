using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DocumentoDbContext : DbContext
    {
        public DocumentoDbContext (DbContextOptions<DocumentoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Entities.Documento> Documento { get; set; } = default!;
    }
}