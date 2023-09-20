using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.AccessData.Migrations
{
    /// <inheritdoc />
    public partial class nameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cabins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_Name",
                table: "Cabins",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_NuRoom",
                table: "Cabins",
                column: "NuRoom",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cabins_Name",
                table: "Cabins");

            migrationBuilder.DropIndex(
                name: "IX_Cabins_NuRoom",
                table: "Cabins");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cabins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
