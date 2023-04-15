using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodNetwork.Migrations
{
    public partial class Doctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Clinic");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Doctor");

            migrationBuilder.AddColumn<decimal>(
                name: "Phone",
                table: "Clinic",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
