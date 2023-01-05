﻿// <auto-generated />
using CatalogoLivros.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CatalogoLivros.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230104113213_Price-decimal")]
    partial class Pricedecimal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CatalogoLivros.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<long>("Isbn")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Harper Lee",
                            Isbn = 9789896412746L,
                            Price = 16.20m,
                            Title = "Mataram a Cotovia",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 2,
                            Author = "Fiódor Dostoiévski",
                            Isbn = 9789896410803L,
                            Price = 20.19m,
                            Title = "Crime e Castigo",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 3,
                            Author = "Miguel de Cervantes",
                            Isbn = 9789720730237L,
                            Price = 8.7m,
                            Title = "Histórias de Dom Quixote",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 4,
                            Author = "Jonathan Black",
                            Isbn = 9789526458521L,
                            Price = 20.19m,
                            Title = "História Secreta do Mundo",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 5,
                            Author = "Jean-François Marmion",
                            Isbn = 9789899033214L,
                            Price = 18.9m,
                            Title = "A Psicologia da Estupidez",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 6,
                            Author = "Ricardo Araújo Pereira",
                            Isbn = 9789896714536L,
                            Price = 12.00m,
                            Title = "Estar Vivo Aleija",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 7,
                            Author = "Daron Acemoglu",
                            Isbn = 9789896441975L,
                            Price = 24.40m,
                            Title = "Porque Falham as Nações",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 8,
                            Author = "Steven Levitsky",
                            Isbn = 9789896684662L,
                            Price = 15.50m,
                            Title = "Como Morrem as democracias",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 9,
                            Author = "Pedro Baños",
                            Isbn = 9789897244315L,
                            Price = 18.5m,
                            Title = "Os donos do mundo",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 10,
                            Author = "Virginia Woolf",
                            Isbn = 9789720726803L,
                            Price = 5.94m,
                            Title = "A Viúva e o Papagaio",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 11,
                            Author = "Paulo Coelho",
                            Isbn = 9789722524223L,
                            Price = 8.00m,
                            Title = "O alquimista",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 12,
                            Author = "Stephen Hawking",
                            Isbn = 9789896162931L,
                            Price = 7.89m,
                            Title = "A teoria de tudo",
                            isDeleted = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}