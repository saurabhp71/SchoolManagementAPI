using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangecolumnnamecountryIdtoCityID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Countries_CountryId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Employees",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CountryId",
                table: "Employees",
                newName: "IX_Employees_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Cities_CityId",
                table: "Employees",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Cities_CityId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Employees",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CityId",
                table: "Employees",
                newName: "IX_Employees_CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Countries_CountryId",
                table: "Employees",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
