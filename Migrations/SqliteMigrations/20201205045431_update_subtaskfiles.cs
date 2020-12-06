using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class update_subtaskfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "content_type",
                table: "wo_task_sub_file",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "upload_type",
                table: "wo_task_sub_file",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content_type",
                table: "wo_task_sub_file");

            migrationBuilder.DropColumn(
                name: "upload_type",
                table: "wo_task_sub_file");
        }
    }
}
