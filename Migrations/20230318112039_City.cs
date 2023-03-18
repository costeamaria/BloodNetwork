using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodNetwork.Migrations
{
    public partial class City : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "Clinic",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Phone",
                table: "Clinic",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_CityID",
                table: "Clinic",
                column: "CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinic_City_CityID",
                table: "Clinic",
                column: "CityID",
                principalTable: "City",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinic_City_CityID",
                table: "Clinic");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropIndex(
                name: "IX_Clinic_CityID",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Clinic");
        }
    }
}
