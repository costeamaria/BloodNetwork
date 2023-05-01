using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodNetwork.Migrations
{
    public partial class Appointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Appointment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentTime",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
