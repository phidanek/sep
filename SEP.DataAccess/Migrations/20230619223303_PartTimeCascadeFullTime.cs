using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PartTimeCascadeFullTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WeekHour",
                columns: new[] { "Id", "JobTypeId", "Name" },
                values: new object[] { 6, 2, "Fulltime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeekHour",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
