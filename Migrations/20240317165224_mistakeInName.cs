using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class mistakeInName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UGotFriendRequestModels_Users_UserId",
                table: "UGotFriendRequestModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UGotFriendRequestModels",
                table: "UGotFriendRequestModels");

            migrationBuilder.RenameTable(
                name: "UGotFriendRequestModels",
                newName: "GotFriendRequestModels");

            migrationBuilder.RenameIndex(
                name: "IX_UGotFriendRequestModels_UserId",
                table: "GotFriendRequestModels",
                newName: "IX_GotFriendRequestModels_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GotFriendRequestModels",
                table: "GotFriendRequestModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GotFriendRequestModels_Users_UserId",
                table: "GotFriendRequestModels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GotFriendRequestModels_Users_UserId",
                table: "GotFriendRequestModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GotFriendRequestModels",
                table: "GotFriendRequestModels");

            migrationBuilder.RenameTable(
                name: "GotFriendRequestModels",
                newName: "UGotFriendRequestModels");

            migrationBuilder.RenameIndex(
                name: "IX_GotFriendRequestModels_UserId",
                table: "UGotFriendRequestModels",
                newName: "IX_UGotFriendRequestModels_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UGotFriendRequestModels",
                table: "UGotFriendRequestModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UGotFriendRequestModels_Users_UserId",
                table: "UGotFriendRequestModels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
