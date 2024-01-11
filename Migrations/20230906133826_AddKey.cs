using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram.Migrations
{
    /// <inheritdoc />
    public partial class AddKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentResponse_Comments_CommentId",
                table: "CommentResponse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentResponse",
                table: "CommentResponse");

            migrationBuilder.RenameTable(
                name: "CommentResponse",
                newName: "CommentResponses");

            migrationBuilder.RenameIndex(
                name: "IX_CommentResponse_CommentId",
                table: "CommentResponses",
                newName: "IX_CommentResponses_CommentId");

            migrationBuilder.AddColumn<int>(
                name: "LikedThingId",
                table: "UsersLiked",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentResponses",
                table: "CommentResponses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentResponses_Comments_CommentId",
                table: "CommentResponses",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentResponses_Comments_CommentId",
                table: "CommentResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentResponses",
                table: "CommentResponses");

            migrationBuilder.DropColumn(
                name: "LikedThingId",
                table: "UsersLiked");

            migrationBuilder.RenameTable(
                name: "CommentResponses",
                newName: "CommentResponse");

            migrationBuilder.RenameIndex(
                name: "IX_CommentResponses_CommentId",
                table: "CommentResponse",
                newName: "IX_CommentResponse_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentResponse",
                table: "CommentResponse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentResponse_Comments_CommentId",
                table: "CommentResponse",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
