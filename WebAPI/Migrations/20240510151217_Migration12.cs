﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration12 : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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