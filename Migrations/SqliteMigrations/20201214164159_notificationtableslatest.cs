using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class notificationtableslatest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notification_setting",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    reminder = table.Column<int>(nullable: false),
                    reminderType = table.Column<int>(nullable: false),
                    notification = table.Column<int>(nullable: false),
                    notificationType = table.Column<int>(nullable: false),
                    remindBeforeAfter = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_setting", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notification_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reminder_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reminder_type", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification_setting");

            migrationBuilder.DropTable(
                name: "notification_type");

            migrationBuilder.DropTable(
                name: "reminder_type");
        }
    }
}
