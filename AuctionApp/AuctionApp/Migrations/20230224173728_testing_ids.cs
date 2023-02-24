using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionApp.Migrations
{
    public partial class testing_ids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItems_AspNetUsers_buyerUserId",
                table: "AuctionItems");

            migrationBuilder.AlterColumn<string>(
                name: "buyerUserId",
                table: "AuctionItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItems_AspNetUsers_buyerUserId",
                table: "AuctionItems",
                column: "buyerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItems_AspNetUsers_buyerUserId",
                table: "AuctionItems");

            migrationBuilder.AlterColumn<string>(
                name: "buyerUserId",
                table: "AuctionItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItems_AspNetUsers_buyerUserId",
                table: "AuctionItems",
                column: "buyerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
