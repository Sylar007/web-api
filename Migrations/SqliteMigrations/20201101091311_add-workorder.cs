using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class addworkorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "qrcode_link",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    qr_id = table.Column<string>(nullable: true),
                    qr_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qrcode_link", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_action",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_action", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_comment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wo_id = table.Column<int>(nullable: false),
                    comment = table.Column<string>(nullable: true),
                    comment_type = table.Column<string>(nullable: true),
                    dt_created = table.Column<DateTime>(nullable: true),
                    dt_modified = table.Column<DateTime>(nullable: true),
                    dt_deleted = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<int>(nullable: true),
                    modified_by = table.Column<int>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_comment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_file",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wo_id = table.Column<int>(nullable: false),
                    media_id = table.Column<int>(nullable: false),
                    file_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_file", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_link",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wo_id = table.Column<int>(nullable: false),
                    link = table.Column<string>(nullable: true),
                    link_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_link", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_part",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wo_id = table.Column<int>(nullable: false),
                    part_id = table.Column<int>(nullable: false),
                    wo_part_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_part", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_priority",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_priority", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_status",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_task_check",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wo_id = table.Column<int>(nullable: false),
                    task_check_id = table.Column<int>(nullable: false),
                    wo_task_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_task_check", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_task_sub",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wo_id = table.Column<int>(nullable: false),
                    task_sub_id = table.Column<int>(nullable: false),
                    wo_task_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_task_sub", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_task_sub_file",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wo_id = table.Column<int>(nullable: false),
                    task_sub_id = table.Column<int>(nullable: false),
                    media_id = table.Column<int>(nullable: false),
                    file_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_task_sub_file", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wo_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    type_desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wo_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_order",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wo_name = table.Column<string>(nullable: true),
                    wo_no = table.Column<string>(nullable: true),
                    wo_type_id = table.Column<int>(nullable: false),
                    equipment_id = table.Column<int>(nullable: false),
                    wo_status_id = table.Column<int>(nullable: false),
                    wo_action_id = table.Column<int>(nullable: false),
                    assignee_user_id = table.Column<int>(nullable: false),
                    approve_user_id = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(nullable: true),
                    action_taken = table.Column<string>(nullable: true),
                    reason_rejected = table.Column<string>(nullable: true),
                    wo_priority_id = table.Column<int>(nullable: false),
                    freq_period_id = table.Column<int>(nullable: false),
                    freq_total = table.Column<int>(nullable: false),
                    reminder_total = table.Column<int>(nullable: false),
                    reminder_period_id = table.Column<int>(nullable: false),
                    dt_start_planned = table.Column<DateTime>(nullable: false),
                    dt_end_planned = table.Column<DateTime>(nullable: false),
                    dt_start_actual = table.Column<DateTime>(nullable: true),
                    dt_end_actual = table.Column<DateTime>(nullable: true),
                    series_seq = table.Column<int>(nullable: true),
                    series_no = table.Column<int>(nullable: true),
                    daily_no = table.Column<int>(nullable: true),
                    ex_photo_data = table.Column<string>(nullable: true),
                    ex_sign_data = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_work_order", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "qrcode_link");

            migrationBuilder.DropTable(
                name: "wo_action");

            migrationBuilder.DropTable(
                name: "wo_comment");

            migrationBuilder.DropTable(
                name: "wo_file");

            migrationBuilder.DropTable(
                name: "wo_link");

            migrationBuilder.DropTable(
                name: "wo_part");

            migrationBuilder.DropTable(
                name: "wo_priority");

            migrationBuilder.DropTable(
                name: "wo_status");

            migrationBuilder.DropTable(
                name: "wo_task_check");

            migrationBuilder.DropTable(
                name: "wo_task_sub");

            migrationBuilder.DropTable(
                name: "wo_task_sub_file");

            migrationBuilder.DropTable(
                name: "wo_type");

            migrationBuilder.DropTable(
                name: "work_order");
        }
    }
}
