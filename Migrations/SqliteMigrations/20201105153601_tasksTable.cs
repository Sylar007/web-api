using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class tasksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    task_no = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    equipment_model_id = table.Column<int>(nullable: false),
                    wo_type_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<sbyte>(nullable: false),
                    dt_created = table.Column<DateTime>(nullable: true),
                    dt_modified = table.Column<DateTime>(nullable: true),
                    dt_deleted = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<int>(nullable: true),
                    modified_by = table.Column<int>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "task_check",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    task_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    dt_created = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<int>(nullable: true),
                    dt_modified = table.Column<DateTime>(nullable: true),
                    modified_by = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_check", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "task_sub",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    task_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    dt_created = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<int>(nullable: true),
                    dt_modified = table.Column<DateTime>(nullable: true),
                    modified_by = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_sub", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropTable(
                name: "task_check");

            migrationBuilder.DropTable(
                name: "task_sub");
        }
    }
}
