using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserIDToJobPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "JobPost",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_JobPost_ApplicationUserId",
                table: "JobPost",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPost_AspNetUsers_ApplicationUserId",
                table: "JobPost",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPost_AspNetUsers_ApplicationUserId",
                table: "JobPost");

            migrationBuilder.DropIndex(
                name: "IX_JobPost_ApplicationUserId",
                table: "JobPost");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "JobPost");
        }
    }
}
