using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SEP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDocument_AspNetUsers_ApplicationUserId",
                table: "ApplicationDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentApplication_Department_DepartmentId",
                table: "StudentApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentApplication_Status_StatusId",
                table: "StudentApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentApplication_WeekHour_WeekHourId",
                table: "StudentApplication");

            migrationBuilder.DropIndex(
                name: "IX_StudentApplication_DepartmentId",
                table: "StudentApplication");

            migrationBuilder.DropIndex(
                name: "IX_StudentApplication_WeekHourId",
                table: "StudentApplication");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationDocument_ApplicationUserId",
                table: "ApplicationDocument");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "StudentApplication");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "StudentApplication");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "StudentApplication");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "StudentApplication");

            migrationBuilder.DropColumn(
                name: "WeekHourId",
                table: "StudentApplication");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ApplicationDocument");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "StudentApplication",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "ApplicationDocument",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApplicationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApplicationStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Rejected" },
                    { 4, "Withdrawn" }
                });

            migrationBuilder.InsertData(
                table: "DriverLicense",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "None" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDocument_ApplicationId",
                table: "ApplicationDocument",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDocument_StudentApplication_ApplicationId",
                table: "ApplicationDocument",
                column: "ApplicationId",
                principalTable: "StudentApplication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentApplication_ApplicationStatus_StatusId",
                table: "StudentApplication",
                column: "StatusId",
                principalTable: "ApplicationStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDocument_StudentApplication_ApplicationId",
                table: "ApplicationDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentApplication_ApplicationStatus_StatusId",
                table: "StudentApplication");

            migrationBuilder.DropTable(
                name: "ApplicationStatus");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationDocument_ApplicationId",
                table: "ApplicationDocument");

            migrationBuilder.DeleteData(
                table: "DriverLicense",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "ApplicationDocument");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "StudentApplication",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "StudentApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "StudentApplication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "StudentApplication",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "StudentApplication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WeekHourId",
                table: "StudentApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ApplicationDocument",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplication_DepartmentId",
                table: "StudentApplication",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplication_WeekHourId",
                table: "StudentApplication",
                column: "WeekHourId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDocument_ApplicationUserId",
                table: "ApplicationDocument",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDocument_AspNetUsers_ApplicationUserId",
                table: "ApplicationDocument",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentApplication_Department_DepartmentId",
                table: "StudentApplication",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentApplication_Status_StatusId",
                table: "StudentApplication",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentApplication_WeekHour_WeekHourId",
                table: "StudentApplication",
                column: "WeekHourId",
                principalTable: "WeekHour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
