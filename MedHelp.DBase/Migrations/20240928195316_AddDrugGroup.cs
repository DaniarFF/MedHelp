using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedHelp.DBase.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Drugs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DrugsGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugsGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_GroupId",
                table: "Drugs",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_DrugsGroups_GroupId",
                table: "Drugs",
                column: "GroupId",
                principalTable: "DrugsGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_DrugsGroups_GroupId",
                table: "Drugs");

            migrationBuilder.DropTable(
                name: "DrugsGroups");

            migrationBuilder.DropIndex(
                name: "IX_Drugs_GroupId",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Drugs");
        }
    }
}
