using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryService.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAddPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                schema: "dbo",
                table: "warehouse",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                schema: "dbo",
                table: "warehouse",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "dbo",
                table: "warehouse");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "dbo",
                table: "warehouse");
        }
    }
}
