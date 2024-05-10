using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "FacialRecognitionId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FacialRecognitionId",
                table: "EventAttendances",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FacialRecognitionId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FacialRecognitionId",
                table: "EventAttendances",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
