using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterHealthCareAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFileNameToMedicalFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "MedicalFiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "MedicalFiles");
        }
    }
}
