using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline_DE.Migrations
{
    public partial class Changesnews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassengerAsignments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "FlightPlannings");

            migrationBuilder.RenameColumn(
                name: "Addres",
                table: "Passengers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "StarTime",
                table: "FlightPlannings",
                newName: "StartTime");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Passengers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FlightPlanningId",
                table: "Passengers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Passengers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Assignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "FlightPlanningId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Passengers",
                newName: "Addres");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "FlightPlannings",
                newName: "StarTime");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "FlightPlannings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PassengerAsignments",
                columns: table => new
                {
                    PassengerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerAsignments", x => x.PassengerId);
                });
        }
    }
}
