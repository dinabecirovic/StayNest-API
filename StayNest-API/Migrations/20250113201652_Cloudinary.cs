using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayNest_API.Migrations
{
    /// <inheritdoc />
    public partial class Cloudinary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Administrators_AdministratorId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_BungalowOwners_BungalowOwnerId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_AdministratorId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_BungalowOwnerId",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "UrlPhoto",
                table: "Advertisements",
                newName: "UrlPhotos");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "BungalowOwners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Administrators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "BungalowOwners");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Administrators");

            migrationBuilder.RenameColumn(
                name: "UrlPhotos",
                table: "Advertisements",
                newName: "UrlPhoto");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_AdministratorId",
                table: "UserRole",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_BungalowOwnerId",
                table: "UserRole",
                column: "BungalowOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Administrators_AdministratorId",
                table: "UserRole",
                column: "AdministratorId",
                principalTable: "Administrators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_BungalowOwners_BungalowOwnerId",
                table: "UserRole",
                column: "BungalowOwnerId",
                principalTable: "BungalowOwners",
                principalColumn: "Id");
        }
    }
}
