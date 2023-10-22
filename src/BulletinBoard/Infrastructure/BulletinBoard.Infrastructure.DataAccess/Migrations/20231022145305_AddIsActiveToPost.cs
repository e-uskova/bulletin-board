using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Posts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Posts");
        }
    }
}
