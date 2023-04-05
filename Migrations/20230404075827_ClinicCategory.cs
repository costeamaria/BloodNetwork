using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodNetwork.Migrations
{
    public partial class ClinicCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinic_City_CityID",
                table: "Clinic");

            migrationBuilder.DropTable(
                name: "BloodCategory");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropIndex(
                name: "IX_Clinic_CityID",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "Clinic");

            migrationBuilder.CreateTable(
                name: "ClinicCategory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClinicCategory_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicCategory_Clinic_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinic",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicCategory_CategoryID",
                table: "ClinicCategory",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicCategory_ClinicID",
                table: "ClinicCategory",
                column: "ClinicID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicCategory");

            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "Clinic",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BloodCategory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ClinicID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BloodCategory_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodCategory_Clinic_BloodID",
                        column: x => x.BloodID,
                        principalTable: "Clinic",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BloodCategory_BloodID",
                table: "BloodCategory",
                column: "BloodID");

            migrationBuilder.CreateIndex(
                name: "IX_BloodCategory_CategoryID",
                table: "BloodCategory",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinic_City_CityID",
                table: "Clinic",
                column: "CityID",
                principalTable: "City",
                principalColumn: "ID");
        }
    }
}
