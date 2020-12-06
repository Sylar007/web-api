using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class addpartmodel_additionalagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "part_model_link",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    part_model_id = table.Column<int>(nullable: false),
                    link = table.Column<string>(nullable: true),
                    link_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_model_link", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "part_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    dt_created = table.Column<DateTime>(nullable: false),
                    dt_modified = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    modified_by = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_type", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "part_model_link");

            migrationBuilder.DropTable(
                name: "part_type");
        }
    }
}
