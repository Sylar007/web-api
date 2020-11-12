using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class updateWoFileField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "content_type",
                table: "wo_file",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content_type",
                table: "wo_file");
        }
    }
}
