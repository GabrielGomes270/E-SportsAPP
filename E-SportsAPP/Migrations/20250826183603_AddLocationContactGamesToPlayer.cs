using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_SportsAPP.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationContactGamesToPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Players",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Games",
                table: "Players",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Players",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Games",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Players");
        }
    }
}
