using CatalogoLivros.Entity;
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
            modelBuilder.Entity<Book>().Property<bool>("isDeleted");
            modelBuilder.Entity<Book>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);


            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Isbn = 9789896412746,
                    Title = "Mataram a Cotovia",
                    Author = "Harper Lee",
                    Price = 16.20M
                },
                new Book
                {
                    Id = 2,
                    Isbn = 9789896410803,
                    Title = "Crime e Castigo",
                    Author = "Fiódor Dostoiévski",
                    Price = 20.19M
                },
                new Book
                {
                    Id = 3,
                    Isbn = 9789720730237,
                    Title = "Histórias de Dom Quixote",
                    Author = "Miguel de Cervantes",
                    Price = 8.7M
                },
                new Book
                {
                    Id = 4,
                    Isbn = 9789526458521,
                    Title = "História Secreta do Mundo",
                    Author = "Jonathan Black",
                    Price = 20.19M
                },
                new Book
                {
                    Id = 5,
                    Isbn = 9789899033214,
                    Title = "A Psicologia da Estupidez",
                    Author = "Jean-François Marmion",
                    Price = 18.9M
                },
                new Book
                {
                    Id = 6,
                    Isbn = 9789896714536,
                    Title = "Estar Vivo Aleija",
                    Author = "Ricardo Araújo Pereira",
                    Price = 12.00M
                },
                new Book
                {
                    Id = 7,
                    Isbn = 9789896441975,
                    Title = "Porque Falham as Nações",
                    Author = "Daron Acemoglu",
                    Price = 24.40M
                },
                new Book
                {
                    Id = 8,
                    Isbn = 9789896684662,
                    Title = "Como Morrem as democracias",
                    Author = "Steven Levitsky",
                    Price = 15.50M
                },
                new Book
                {
                    Id = 9,
                    Isbn = 9789897244315,
                    Title = "Os donos do mundo",
                    Author = "Pedro Baños",
                    Price = 18.5M
                },
                new Book
                {
                    Id = 10,
                    Isbn = 9789720726803,
                    Title = "A Viúva e o Papagaio",
                    Author = "Virginia Woolf",
                    Price = 5.94M
                },
                new Book
                {
                    Id = 11,
                    Isbn = 9789722524223,
                    Title = "O alquimista",
                    Author = "Paulo Coelho",
                    Price = 8.00M
                },
                new Book
                {
                    Id = 12,
                    Isbn = 9789896162931,
                    Title = "A teoria de tudo",
                    Author = "Stephen Hawking",
                    Price = 7.89M
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

