using Microsoft.EntityFrameworkCore;
using ATI_IEC.Models;

namespace ATI_IEC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserReader> UserReaders { get; set; } = null!;
        public DbSet<IecDocument> IecDocuments { get; set; } = null!;
        public DbSet<KpsRequest> KpsRequests { get; set; } = null!;
    }
}
