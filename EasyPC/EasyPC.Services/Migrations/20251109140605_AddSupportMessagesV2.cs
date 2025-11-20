using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPC.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddSupportMessagesV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "SupportMessages",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_SupportMessages_UserId",
                table: "SupportMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportMessages_Users_UserId",
                table: "SupportMessages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportMessages_Users_UserId",
                table: "SupportMessages");

            migrationBuilder.DropIndex(
                name: "IX_SupportMessages_UserId",
                table: "SupportMessages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "SupportMessages",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");
        }
    }
}
