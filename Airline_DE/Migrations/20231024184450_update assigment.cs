using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline_DE.Migrations
{
    public partial class updateassigment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceptorId",
                table: "Assignments",
                newName: "ReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Assignments",
                newName: "ReceptorId");
        }
    }
}
