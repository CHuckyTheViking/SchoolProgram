using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolProgram.Data.Migrations
{
    public partial class inClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "inClass",
                table: "AspNetUsers",
                type: "varchar(5)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inClass",
                table: "AspNetUsers");
        }
    }
}
