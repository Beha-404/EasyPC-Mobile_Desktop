using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPC.Services.Migrations
{
    /// <inheritdoc />
    public partial class updateV10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Rams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Processors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "PowerSupplies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "PcTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "PCs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Motherboards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Manufacturers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "GraphicsCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PowerSupplies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PcTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PCs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GraphicsCards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cases");
        }
    }
}
