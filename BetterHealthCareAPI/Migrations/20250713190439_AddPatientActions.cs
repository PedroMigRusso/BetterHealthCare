using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterHealthCareAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalFiles_PatientActions_PatientActionId",
                table: "MedicalFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientActions_Patients_PatientId",
                table: "PatientActions");

            migrationBuilder.DropIndex(
                name: "IX_MedicalFiles_PatientActionId",
                table: "MedicalFiles");

            migrationBuilder.DropColumn(
                name: "Hospital",
                table: "PatientActions");

            migrationBuilder.DropColumn(
                name: "PatientActionId",
                table: "MedicalFiles");

            migrationBuilder.RenameColumn(
                name: "OperationTeam",
                table: "PatientActions",
                newName: "FilesId");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "PatientActions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientActions_Patients_PatientId",
                table: "PatientActions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientActions_Patients_PatientId",
                table: "PatientActions");

            migrationBuilder.RenameColumn(
                name: "FilesId",
                table: "PatientActions",
                newName: "OperationTeam");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "PatientActions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Hospital",
                table: "PatientActions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientActionId",
                table: "MedicalFiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalFiles_PatientActionId",
                table: "MedicalFiles",
                column: "PatientActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalFiles_PatientActions_PatientActionId",
                table: "MedicalFiles",
                column: "PatientActionId",
                principalTable: "PatientActions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientActions_Patients_PatientId",
                table: "PatientActions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
