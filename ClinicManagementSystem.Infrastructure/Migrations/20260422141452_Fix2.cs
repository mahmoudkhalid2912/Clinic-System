using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_Day",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Day",
                table: "Schedules",
                column: "Day");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_Day",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Day",
                table: "Schedules",
                column: "Day",
                unique: true);
        }
    }
}
