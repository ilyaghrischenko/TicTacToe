using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToe.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserClassRemovedGameRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_GameRoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_GameRoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GameRoleId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameRoleId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<int>(type: "int", nullable: false),
                    Statuses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    countVotes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_GameRoleId",
                table: "Users",
                column: "GameRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_GameRoleId",
                table: "Users",
                column: "GameRoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
