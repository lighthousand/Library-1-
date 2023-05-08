using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class LibraryDbContext: DbContext
    {
        public LibraryDbContext(DbContextOptions options): base(options)
        {            
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
                .Property(p => p.Authors)
                .HasConversion(
                c => string.Join(',', c),
                c => c.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
