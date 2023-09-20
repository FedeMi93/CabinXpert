using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.AccessData.Migrations
{
    /// <inheritdoc />
    public partial class ValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Password_Value");

            migrationBuilder.RenameColumn(
                name: "NameWorker",
                table: "Maintenances",
                newName: "NameOfWorker_Value");

            migrationBuilder.RenameColumn(
                name: "CostPerson",
                table: "CabinTypes",
                newName: "CostPerson_Value");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Cabins",
                newName: "Capacity_Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password_Value",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "NameOfWorker_Value",
                table: "Maintenances",
                newName: "NameWorker");

            migrationBuilder.RenameColumn(
                name: "CostPerson_Value",
                table: "CabinTypes",
                newName: "CostPerson");

            migrationBuilder.RenameColumn(
                name: "Capacity_Value",
                table: "Cabins",
                newName: "Capacity");
        }
    }
}
