using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class changeSomeErrors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Friends_FriendId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FriendId",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Messages_FriendId",
                table: "Messages",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Friends_FriendId",
                table: "Messages",
                column: "FriendId",
                principalTable: "Friends",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
