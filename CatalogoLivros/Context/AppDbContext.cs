using CatalogoLivros.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoLivros.Context
{
    public class AppDbContext : DbContext
    {
    public DbSet<Book> Books { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = CatalogoLivros; Integrated Security = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Isbn = 9789896412746,
                    Title = "Mataram a Cotovia",
                    Author = "Harper Lee",
                    Price = 16.20
                },
                new Book
                {
                    Id = 2,
                    Isbn = 9789896410803,
                    Title = "Crime e Castigo",
                    Author = "Fiódor Dostoiévski",
                    Price = 20.19
                }

            );
        }

    }
}

