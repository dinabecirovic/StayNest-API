using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayNest_API.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_BungalowOwners_BungalowOwnerId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UsersId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UsersId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_BungalowOwnerId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "UserRole");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "UserRole",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UsersId",
                table: "UserRole",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_BungalowOwnerId",
                table: "Advertisements",
                column: "BungalowOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_BungalowOwners_BungalowOwnerId",
                table: "Advertisements",
                column: "BungalowOwnerId",
                principalTable: "BungalowOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UsersId",
                table: "UserRole",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
