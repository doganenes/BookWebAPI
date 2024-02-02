using BookWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookWebApi.Repositories
{
    public class BaseDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; database=BookApiDb;Trusted_Connection=true");
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Author> Authors { get; set; }
    }   
}
