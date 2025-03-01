using Microsoft.EntityFrameworkCore.Migrations;

namespace learn_asp.Migrations
{
    public partial class AddEmailColumnToRegisteredUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "RegisteredUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "RegisteredUsers");
        }
    }
}
