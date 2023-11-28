using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Identity.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNewFiedls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardInfo_AspNetUsers_AuthUserId",
                table: "CardInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CardInfo_AspNetUsers_UserId",
                table: "CardInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardInfo",
                table: "CardInfo");

            migrationBuilder.RenameTable(
                name: "CardInfo",
                newName: "CardInfos");

            migrationBuilder.RenameIndex(
                name: "IX_CardInfo_UserId",
                table: "CardInfos",
                newName: "IX_CardInfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CardInfo_AuthUserId",
                table: "CardInfos",
                newName: "IX_CardInfos_AuthUserId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardInfos",
                table: "CardInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardInfos_AspNetUsers_AuthUserId",
                table: "CardInfos",
                column: "AuthUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardInfos_AspNetUsers_UserId",
                table: "CardInfos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardInfos_AspNetUsers_AuthUserId",
                table: "CardInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_CardInfos_AspNetUsers_UserId",
                table: "CardInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardInfos",
                table: "CardInfos");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "CardInfos",
                newName: "CardInfo");

            migrationBuilder.RenameIndex(
                name: "IX_CardInfos_UserId",
                table: "CardInfo",
                newName: "IX_CardInfo_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CardInfos_AuthUserId",
                table: "CardInfo",
                newName: "IX_CardInfo_AuthUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardInfo",
                table: "CardInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardInfo_AspNetUsers_AuthUserId",
                table: "CardInfo",
                column: "AuthUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardInfo_AspNetUsers_UserId",
                table: "CardInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
