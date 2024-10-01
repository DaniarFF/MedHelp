using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHelp.DBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDrugColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Form",
                table: "Drugs");

            migrationBuilder.AlterColumn<long>(
                name: "TelegramId",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TelegramId",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Form",
                table: "Drugs",
                type: "text",
                nullable: true);
        }
    }
}
