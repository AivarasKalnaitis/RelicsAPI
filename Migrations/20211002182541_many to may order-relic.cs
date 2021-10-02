using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelicsAPI.Migrations
{
    public partial class manytomayorderrelic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderRelic",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    RelicsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRelic", x => new { x.OrdersId, x.RelicsId });
                    table.ForeignKey(
                        name: "FK_OrderRelic_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRelic_Relics_RelicsId",
                        column: x => x.RelicsId,
                        principalTable: "Relics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderRelic_RelicsId",
                table: "OrderRelic",
                column: "RelicsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRelic");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
