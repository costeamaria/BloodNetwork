using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodNetwork.Migrations
{
    public partial class Adress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdressID",
                table: "Clinic",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "Clinic",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Adress",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdressName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adress", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_AdressID",
                table: "Clinic",
                column: "AdressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinic_Adress_AdressID",
                table: "Clinic",
                column: "AdressID",
                principalTable: "Adress",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinic_Adress_AdressID",
                table: "Clinic");

            migrationBuilder.DropTable(
                name: "Adress");

            migrationBuilder.DropIndex(
                name: "IX_Clinic_AdressID",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "AdressID",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Clinic");
        }
    }
}
