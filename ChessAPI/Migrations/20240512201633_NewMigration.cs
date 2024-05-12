using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameResult",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameStatus",
                table: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameResult",
                table: "Games",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "GameStatus",
                table: "Games",
                type: "longtext",
                nullable: false);
        }
    }
}
