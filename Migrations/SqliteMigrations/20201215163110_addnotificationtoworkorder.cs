using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class addnotificationtoworkorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "notification_id",
                table: "work_order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "notification_id",
                table: "work_order");
        }
    }
}
