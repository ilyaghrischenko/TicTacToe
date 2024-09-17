using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mafia.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Statistics_StatisticId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "StatisticId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Statistics_StatisticId",
                table: "Users",
                column: "StatisticId",
                principalTable: "Statistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Statistics_StatisticId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "StatisticId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Statistics_StatisticId",
                table: "Users",
                column: "StatisticId",
                principalTable: "Statistics",
                principalColumn: "Id");
        }
    }
}
