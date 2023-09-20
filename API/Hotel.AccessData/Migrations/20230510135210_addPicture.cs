using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.AccessData.Migrations
{
    /// <inheritdoc />
    public partial class addPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CabinTypes",
                newName: "Type Name");

            migrationBuilder.RenameIndex(
                name: "IX_CabinTypes_Name",
                table: "CabinTypes",
                newName: "IX_CabinTypes_Type Name");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Cabins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Cabins");

            migrationBuilder.RenameColumn(
                name: "Type Name",
                table: "CabinTypes",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_CabinTypes_Type Name",
                table: "CabinTypes",
                newName: "IX_CabinTypes_Name");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
