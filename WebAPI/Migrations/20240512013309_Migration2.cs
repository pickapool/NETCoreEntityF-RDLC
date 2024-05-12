using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSanctions_Accounts_UserId",
                table: "UserSanctions");

            migrationBuilder.DropIndex(
                name: "IX_UserSanctions_UserId",
                table: "UserSanctions");

            migrationBuilder.AddColumn<int>(
                name: "AccountUserId",
                table: "UserSanctions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserSanctions_AccountUserId",
                table: "UserSanctions",
                column: "AccountUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSanctions_Accounts_AccountUserId",
                table: "UserSanctions",
                column: "AccountUserId",
                principalTable: "Accounts",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSanctions_Accounts_AccountUserId",
                table: "UserSanctions");

            migrationBuilder.DropIndex(
                name: "IX_UserSanctions_AccountUserId",
                table: "UserSanctions");

            migrationBuilder.DropColumn(
                name: "AccountUserId",
                table: "UserSanctions");

            migrationBuilder.CreateIndex(
                name: "IX_UserSanctions_UserId",
                table: "UserSanctions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSanctions_Accounts_UserId",
                table: "UserSanctions",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
