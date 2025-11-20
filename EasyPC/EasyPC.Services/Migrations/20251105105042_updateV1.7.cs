using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPC.Services.Migrations
{
    /// <inheritdoc />
    public partial class updateV17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "PCs",
                newName: "RatingCount");

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "PCs",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PCId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PcId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_PCs_PcId",
                        column: x => x.PcId,
                        principalTable: "PCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PCId",
                table: "Orders",
                column: "PCId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PcId",
                table: "Ratings",
                column: "PcId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId_PcId",
                table: "Ratings",
                columns: new[] { "UserId", "PcId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PCs_PCId",
                table: "Orders",
                column: "PCId",
                principalTable: "PCs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PCs_PCId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PCId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "PCs");

            migrationBuilder.DropColumn(
                name: "PCId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "RatingCount",
                table: "PCs",
                newName: "Rating");
        }
    }
}
