using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CatalogoLivros.Migrations
{
    /// <inheritdoc />
    public partial class Pricedecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 16.20m);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 20.19m);

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Isbn", "Price", "Title", "isDeleted" },
                values: new object[,]
                {
                    { 3, "Miguel de Cervantes", 9789720730237L, 8.7m, "Histórias de Dom Quixote", false },
                    { 4, "Jonathan Black", 9789526458521L, 20.19m, "História Secreta do Mundo", false },
                    { 5, "Jean-François Marmion", 9789899033214L, 18.9m, "A Psicologia da Estupidez", false },
                    { 6, "Ricardo Araújo Pereira", 9789896714536L, 12.00m, "Estar Vivo Aleija", false },
                    { 7, "Daron Acemoglu", 9789896441975L, 24.40m, "Porque Falham as Nações", false },
                    { 8, "Steven Levitsky", 9789896684662L, 15.50m, "Como Morrem as democracias", false },
                    { 9, "Pedro Baños", 9789897244315L, 18.5m, "Os donos do mundo", false },
                    { 10, "Virginia Woolf", 9789720726803L, 5.94m, "A Viúva e o Papagaio", false },
                    { 11, "Paulo Coelho", 9789722524223L, 8.00m, "O alquimista", false },
                    { 12, "Stephen Hawking", 9789896162931L, 7.89m, "A teoria de tudo", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Books",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 16.199999999999999);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 20.190000000000001);
        }
    }
}
