using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.AccessData.Migrations
{
    /// <inheritdoc />
    public partial class TypeEnCabin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Cabins");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Cabins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_TypeId",
                table: "Cabins",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabins_CabinTypes_TypeId",
                table: "Cabins",
                column: "TypeId",
                principalTable: "CabinTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabins_CabinTypes_TypeId",
                table: "Cabins");

            migrationBuilder.DropIndex(
                name: "IX_Cabins_TypeId",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Cabins");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Cabins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
