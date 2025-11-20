using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPC.Services.Migrations
{
    /// <inheritdoc />
    public partial class updateV18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PCs_PCId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PCId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PCId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipDate",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "PaymentMethod");

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    PcId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_PCs_PcId",
                        column: x => x.PcId,
                        principalTable: "PCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PcId",
                table: "OrderDetails",
                column: "PcId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Orders",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "PCId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShipDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PCId",
                table: "Orders",
                column: "PCId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PCs_PCId",
                table: "Orders",
                column: "PCId",
                principalTable: "PCs",
                principalColumn: "Id");
        }
    }
}
