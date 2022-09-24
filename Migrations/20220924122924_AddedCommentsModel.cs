using Microsoft.EntityFrameworkCore.Migrations;

namespace LabInsta.Migrations
{
    public partial class AddedCommentsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsersLogin",
                table: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsersLogin",
                table: "Post",
                type: "text",
                nullable: true);
        }
    }
}
