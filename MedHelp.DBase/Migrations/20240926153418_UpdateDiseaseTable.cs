using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHelp.DBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiseaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistinctiveSigns",
                table: "Diseases",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistinctiveSigns",
                table: "Diseases");
        }
    }
}
