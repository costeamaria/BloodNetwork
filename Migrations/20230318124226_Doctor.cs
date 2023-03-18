using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodNetwork.Migrations
{
    public partial class Doctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorID",
                table: "Clinic",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_DoctorID",
                table: "Clinic",
                column: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinic_Doctor_DoctorID",
                table: "Clinic",
                column: "DoctorID",
                principalTable: "Doctor",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinic_Doctor_DoctorID",
                table: "Clinic");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Clinic_DoctorID",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Clinic");
        }
    }
}
