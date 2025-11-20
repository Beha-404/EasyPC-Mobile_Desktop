using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPC.Services.Migrations
{
    /// <inheritdoc />
    public partial class updateV12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "Rams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "Processors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "PowerSupplies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "PcTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "PCs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "Motherboards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "Manufacturers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "GraphicsCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "PowerSupplies");

            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "PcTypes");

            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "PCs");

            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "GraphicsCards");

            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "Cases");
        }
    }
}
