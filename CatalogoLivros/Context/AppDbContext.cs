using CatalogoLivros.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CatalogoLivros.Context
{
    public class AppDbContext : DbContext
    {
    public DbSet<Book> Books { get; set; } = null;
    public DbSet<Author> Authors { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = CatalogoLivros; Integrated Security = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().Property<bool>("isDeleted");
            modelBuilder.Entity<Book>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);

            modelBuilder.Entity<Author>().Property<bool>("isDeleted");
            modelBuilder.Entity<Author>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);


            modelBuilder.Entity<Book>().HasData(

            new Book
            {
                Id = 1,
                Isbn = 9789896412746,
                Title = "Mataram a Cotovia",
                AuthorId = 1,
                Price = 16.20M
            },
            new Book
            {
                Id = 2,
                Isbn = 9789896410803,
                Title = "Crime e Castigo",
                AuthorId = 1,
                Price = 20.19M
            },
            new Book
            {
                Id = 3,
                Isbn = 9789720730237,
                Title = "Histórias de Dom Quixote",
                AuthorId = 1,
                Price = 8.7M
            },
            new Book
            {
                Id = 4,
                Isbn = 9789526458521,
                Title = "História Secreta do Mundo",
                AuthorId = 1,
                Price = 20.19M
            },
            new Book
            {
                Id = 5,
                Isbn = 9789899033214,
                Title = "A Psicologia da Estupidez",
                AuthorId = 1,
                Price = 18.9M
            },
            new Book
            {
                Id = 6,
                Isbn = 9789896714536,
                Title = "Estar Vivo Aleija",
                AuthorId = 1,
                Price = 12.00M
            },
            new Book
            {
                Id = 7,
                Isbn = 9789896441975,
                Title = "Porque Falham as Nações",
                AuthorId = 1,
                Price = 24.40M
            },
            new Book
            {
                Id = 8,
                Isbn = 9789896684662,
                Title = "Como Morrem as democracias",
                AuthorId = 1,
                Price = 15.50M
            },
            new Book
            {
                Id = 9,
                Isbn = 9789897244315,
                Title = "Os donos do mundo",
                AuthorId = 1,
                Price = 18.5M
            },
            new Book
            {
                Id = 10,
                Isbn = 9789720726803,
                Title = "A Viúva e o Papagaio",
                AuthorId = 1,
                Price = 5.94M
            },
            new Book
            {
                Id = 11,
                Isbn = 9789722524223,
                Title = "O alquimista",
                AuthorId = 1,
                Price = 8.00M
            },
            new Book
            {
                Id = 12,
                Isbn = 9789896162931,
                Title = "A teoria de tudo",
                AuthorId = 1,
                Price = 7.89M
            }

            );
            modelBuilder.Entity<Author>().HasData(

                new Author
                {
                    Id = 1,
                    Name = "Harper Lee",
                    Nacionality = "EUA"
                }
            );
        }


        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["isDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["isDeleted"] = true;
                        break;
                }
            }
        


    }

    }
}

