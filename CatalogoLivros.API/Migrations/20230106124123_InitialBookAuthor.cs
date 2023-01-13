using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CatalogoLivros.Migrations
{
    /// <inheritdoc />
    public partial class InitialBookAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isbn = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Harper Lee" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Isbn", "Price", "Title", "isDeleted" },
                values: new object[,]
                {
                    { 1, 1, 9789896412746L, 16.20m, "Mataram a Cotovia", false },
                    { 2, 1, 9789896410803L, 20.19m, "Crime e Castigo", false },
                    { 3, 1, 9789720730237L, 8.7m, "Histórias de Dom Quixote", false },
                    { 4, 1, 9789526458521L, 20.19m, "História Secreta do Mundo", false },
                    { 5, 1, 9789899033214L, 18.9m, "A Psicologia da Estupidez", false },
                    { 6, 1, 9789896714536L, 12.00m, "Estar Vivo Aleija", false },
                    { 7, 1, 9789896441975L, 24.40m, "Porque Falham as Nações", false },
                    { 8, 1, 9789896684662L, 15.50m, "Como Morrem as democracias", false },
                    { 9, 1, 9789897244315L, 18.5m, "Os donos do mundo", false },
                    { 10, 1, 9789720726803L, 5.94m, "A Viúva e o Papagaio", false },
                    { 11, 1, 9789722524223L, 8.00m, "O alquimista", false },
                    { 12, 1, 9789896162931L, 7.89m, "A teoria de tudo", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
