using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class added_token_url_to_settings_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token_end_point",
                table: "integration_settings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "token_url",
                table: "integration_settings",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token_end_point",
                table: "integration_settings");

            migrationBuilder.DropColumn(
                name: "token_url",
                table: "integration_settings");
        }
    }
}
