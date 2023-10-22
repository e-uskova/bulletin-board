using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampsToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Attachments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AuthorId",
                table: "Attachments",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Users_AuthorId",
                table: "Attachments",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Users_AuthorId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_AuthorId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Attachments");
        }
    }
}
