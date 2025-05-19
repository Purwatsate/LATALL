using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryService.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "warehouse",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "warehouse",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "warehouse",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "warehouse",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                schema: "dbo",
                table: "warehouse",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "stock",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "stock",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "stock",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "stock",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                schema: "dbo",
                table: "stock",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "reserved_stock",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "reserved_stock",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "reserved_stock",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "reserved_stock",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                schema: "dbo",
                table: "reserved_stock",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "inventory_transaction",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "inventory_transaction",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "inventory_transaction",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                schema: "dbo",
                table: "inventory_transaction",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "warehouse");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "warehouse");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "warehouse");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "warehouse");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                schema: "dbo",
                table: "warehouse");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "stock");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "stock");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "stock");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "stock");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                schema: "dbo",
                table: "stock");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "reserved_stock");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "reserved_stock");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "reserved_stock");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "reserved_stock");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                schema: "dbo",
                table: "reserved_stock");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "inventory_transaction");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "inventory_transaction");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "inventory_transaction");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                schema: "dbo",
                table: "inventory_transaction");
        }
    }
}
