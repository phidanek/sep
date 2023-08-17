using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PartTimeCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeekHour",
                keyColumn: "Id",
                keyValue: 3,
                column: "JobTypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "WeekHour",
                keyColumn: "Id",
                keyValue: 4,
                column: "JobTypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "WeekHour",
                keyColumn: "Id",
                keyValue: 5,
                column: "JobTypeId",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeekHour",
                keyColumn: "Id",
                keyValue: 3,
                column: "JobTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WeekHour",
                keyColumn: "Id",
                keyValue: 4,
                column: "JobTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WeekHour",
                keyColumn: "Id",
                keyValue: 5,
                column: "JobTypeId",
                value: 2);
        }
    }
}
