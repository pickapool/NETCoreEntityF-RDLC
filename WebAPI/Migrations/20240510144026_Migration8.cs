using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "EventAttendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EventAttendances_StudentId",
                table: "EventAttendances",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttendances_Students_StudentId",
                table: "EventAttendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAttendances_Students_StudentId",
                table: "EventAttendances");

            migrationBuilder.DropIndex(
                name: "IX_EventAttendances_StudentId",
                table: "EventAttendances");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "EventAttendances");
        }
    }
}
