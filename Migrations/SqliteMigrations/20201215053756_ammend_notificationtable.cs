using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class ammend_notificationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "frequency",
                table: "notification_setting",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "frequencyType",
                table: "notification_setting",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "frequency",
                table: "notification_setting");

            migrationBuilder.DropColumn(
                name: "frequencyType",
                table: "notification_setting");
        }
    }
}
