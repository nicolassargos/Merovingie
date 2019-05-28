using Microsoft.EntityFrameworkCore.Migrations;

namespace MerovingieAuth.Data.Migrations
{
    public partial class ajout_ChatNickName_a_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatNickName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatNickName",
                table: "AspNetUsers");
        }
    }
}
