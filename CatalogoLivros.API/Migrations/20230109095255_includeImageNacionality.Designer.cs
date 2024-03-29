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
    [Migration("20230109095255_includeImageNacionality")]
    partial class includeImageNacionality
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CatalogoLivros.Entity.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nacionality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nacionality = "EUA",
                            Name = "Harper Lee",
                            isDeleted = false
                        });
                });

            modelBuilder.Entity("CatalogoLivros.Entity.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<long>("Isbn")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Isbn = 9789896412746L,
                            Price = 16.20m,
                            Title = "Mataram a Cotovia",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            Isbn = 9789896410803L,
                            Price = 20.19m,
                            Title = "Crime e Castigo",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 1,
                            Isbn = 9789720730237L,
                            Price = 8.7m,
                            Title = "Histórias de Dom Quixote",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 1,
                            Isbn = 9789526458521L,
                            Price = 20.19m,
                            Title = "História Secreta do Mundo",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 1,
                            Isbn = 9789899033214L,
                            Price = 18.9m,
                            Title = "A Psicologia da Estupidez",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 6,
                            AuthorId = 1,
                            Isbn = 9789896714536L,
                            Price = 12.00m,
                            Title = "Estar Vivo Aleija",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 7,
                            AuthorId = 1,
                            Isbn = 9789896441975L,
                            Price = 24.40m,
                            Title = "Porque Falham as Nações",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 8,
                            AuthorId = 1,
                            Isbn = 9789896684662L,
                            Price = 15.50m,
                            Title = "Como Morrem as democracias",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 9,
                            AuthorId = 1,
                            Isbn = 9789897244315L,
                            Price = 18.5m,
                            Title = "Os donos do mundo",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 10,
                            AuthorId = 1,
                            Isbn = 9789720726803L,
                            Price = 5.94m,
                            Title = "A Viúva e o Papagaio",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 11,
                            AuthorId = 1,
                            Isbn = 9789722524223L,
                            Price = 8.00m,
                            Title = "O alquimista",
                            isDeleted = false
                        },
                        new
                        {
                            Id = 12,
                            AuthorId = 1,
                            Isbn = 9789896162931L,
                            Price = 7.89m,
                            Title = "A teoria de tudo",
                            isDeleted = false
                        });
                });

            modelBuilder.Entity("CatalogoLivros.Entity.Book", b =>
                {
                    b.HasOne("CatalogoLivros.Entity.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("CatalogoLivros.Entity.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
