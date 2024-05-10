using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "FacialRecognitionId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FacialRecognitionId",
                table: "EventAttendances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Students_FacialRecognitionId",
                table: "Students",
                column: "FacialRecognitionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventAttendances_FacialRecognitionId",
                table: "EventAttendances",
                column: "FacialRecognitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttendances_Students_FacialRecognitionId",
                table: "EventAttendances",
                column: "FacialRecognitionId",
                principalTable: "Students",
                principalColumn: "FacialRecognitionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAttendances_Students_FacialRecognitionId",
                table: "EventAttendances");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Students_FacialRecognitionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_EventAttendances_FacialRecognitionId",
                table: "EventAttendances");

            migrationBuilder.DropColumn(
                name: "FacialRecognitionId",
                table: "EventAttendances");

            migrationBuilder.AlterColumn<string>(
                name: "FacialRecognitionId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
