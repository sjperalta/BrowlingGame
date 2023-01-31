using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrowlingGame.DAL.Migrations
{
    public partial class FixReferencesOnGameModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frames_Games_GameId",
                table: "Frames");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Frames",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Frames_Games_GameId",
                table: "Frames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frames_Games_GameId",
                table: "Frames");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Frames",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Frames_Games_GameId",
                table: "Frames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
