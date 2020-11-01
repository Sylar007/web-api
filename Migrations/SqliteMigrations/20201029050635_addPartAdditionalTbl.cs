using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class addPartAdditionalTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "part_field",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    part_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    field_value = table.Column<string>(nullable: true),
                    field_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_field", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "part_file",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    part_id = table.Column<int>(nullable: false),
                    media_id = table.Column<int>(nullable: false),
                    file_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_file", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "part_link",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    part_id = table.Column<int>(nullable: false),
                    link = table.Column<string>(nullable: true),
                    link_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_link", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "part_field");

            migrationBuilder.DropTable(
                name: "part_file");

            migrationBuilder.DropTable(
                name: "part_link");
        }
    }
}
