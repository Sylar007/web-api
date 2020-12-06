using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class addpartmodel_additional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "part_model_field",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    part_model_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    field_value = table.Column<string>(nullable: true),
                    field_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_model_field", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "part_model_file",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    part_model_id = table.Column<int>(nullable: false),
                    media_id = table.Column<int>(nullable: false),
                    file_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_model_file", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "part_model_field");

            migrationBuilder.DropTable(
                name: "part_model_file");
        }
    }
}
