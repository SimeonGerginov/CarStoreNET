using System;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarStore.Data.Migrations
{
    public partial class AddedShoppingCartAndOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerId1 = table.Column<string>(nullable: true),
                    CarId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 300, nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerId1 = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => new { x.Id, x.CustomerId });
                    table.UniqueConstraint("AK_ShoppingCarts_CustomerId_Id", x => new { x.CustomerId, x.Id });
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(nullable: false),
                    ShoppingCartId1 = table.Column<int>(nullable: true),
                    ShoppingCartCustomerId = table.Column<int>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerId1 = table.Column<string>(nullable: true),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => new { x.CustomerId, x.ShoppingCartId });
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_ShoppingCarts_ShoppingCartId1_ShoppingCartCustomerId",
                        columns: x => new { x.ShoppingCartId1, x.ShoppingCartCustomerId },
                        principalTable: "ShoppingCarts",
                        principalColumns: new[] { "Id", "CustomerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarId1 = table.Column<int>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    ShoppingCartCustomerId = table.Column<int>(nullable: true),
                    ShoppingCartId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Cars_CarId1",
                        column: x => x.CarId1,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId_ShoppingCartCustomerId",
                        columns: x => new { x.ShoppingCartId, x.ShoppingCartCustomerId },
                        principalTable: "ShoppingCarts",
                        principalColumns: new[] { "Id", "CustomerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId1",
                table: "Orders",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShoppingCartId1_ShoppingCartCustomerId",
                table: "Orders",
                columns: new[] { "ShoppingCartId1", "ShoppingCartCustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CarId",
                table: "Reviews",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId1",
                table: "Reviews",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_CarId1",
                table: "ShoppingCartItems",
                column: "CarId1");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId_ShoppingCartCustomerId",
                table: "ShoppingCartItems",
                columns: new[] { "ShoppingCartId", "ShoppingCartCustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CustomerId1",
                table: "ShoppingCarts",
                column: "CustomerId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");
        }
    }
}
