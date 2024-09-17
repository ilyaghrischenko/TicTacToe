using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mafia.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Users_UserId",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Statistics");

            migrationBuilder.AddColumn<int>(
                name: "StatisticId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatisticId",
                table: "Users",
                column: "StatisticId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Statistics_StatisticId",
                table: "Users",
                column: "StatisticId",
                principalTable: "Statistics",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Statistics_StatisticId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StatisticId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StatisticId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Users_UserId",
                table: "Statistics",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
