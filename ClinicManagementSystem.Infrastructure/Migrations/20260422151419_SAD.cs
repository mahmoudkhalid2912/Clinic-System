using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SAD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_PatientId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Bookings",
                newName: "BookedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_PatientId",
                table: "Bookings",
                newName: "IX_Bookings_BookedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_BookedByUserId",
                table: "Bookings",
                column: "BookedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_BookedByUserId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookedByUserId",
                table: "Bookings",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BookedByUserId",
                table: "Bookings",
                newName: "IX_Bookings_PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_PatientId",
                table: "Bookings",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
