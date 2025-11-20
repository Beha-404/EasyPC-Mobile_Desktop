using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPC.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddSenderIdAndConversationUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportMessages_Users_UserId",
                table: "SupportMessages");

            migrationBuilder.DropIndex(
                name: "IX_SupportMessages_UserId",
                table: "SupportMessages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SupportMessages",
                newName: "SenderId");

            migrationBuilder.AddColumn<int>(
                name: "ConversationUserId",
                table: "SupportMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SupportMessages_ConversationUserId",
                table: "SupportMessages",
                column: "ConversationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportMessages_Users_ConversationUserId",
                table: "SupportMessages",
                column: "ConversationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportMessages_Users_ConversationUserId",
                table: "SupportMessages");

            migrationBuilder.DropIndex(
                name: "IX_SupportMessages_ConversationUserId",
                table: "SupportMessages");

            migrationBuilder.DropColumn(
                name: "ConversationUserId",
                table: "SupportMessages");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "SupportMessages",
                newName: "UserId");

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
    }
}
