using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPC.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddPcType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateMachine",
                table: "PcTypes");

            migrationBuilder.DropColumn(
                name: "PCType",
                table: "PCs");

            migrationBuilder.AddColumn<int>(
                name: "PcTypeId",
                table: "PCs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PCs_PcTypeId",
                table: "PCs",
                column: "PcTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PCs_PcTypes_PcTypeId",
                table: "PCs",
                column: "PcTypeId",
                principalTable: "PcTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PCs_PcTypes_PcTypeId",
                table: "PCs");

            migrationBuilder.DropIndex(
                name: "IX_PCs_PcTypeId",
                table: "PCs");

            migrationBuilder.DropColumn(
                name: "PcTypeId",
                table: "PCs");

            migrationBuilder.AddColumn<string>(
                name: "StateMachine",
                table: "PcTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PCType",
                table: "PCs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
