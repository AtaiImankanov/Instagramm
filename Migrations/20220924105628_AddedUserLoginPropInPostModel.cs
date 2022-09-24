using Microsoft.EntityFrameworkCore.Migrations;

namespace LabInsta.Migrations
{
    public partial class AddedUserLoginPropInPostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsersLogin",
                table: "Post",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsersLogin",
                table: "Post");
        }
    }
}
