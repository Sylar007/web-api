using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class UpdateFile_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_equipments_equipment_status_equipment_statusid",
            //    table: "equipments");

            //migrationBuilder.DropIndex(
            //    name: "IX_equipments_equipment_statusid",
            //    table: "equipments");

            //migrationBuilder.DropColumn(
            //    name: "equipment_statusid",
            //    table: "equipments");

            migrationBuilder.AddColumn<string>(
                name: "content_type",
                table: "part_file",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "content_type",
                table: "equipment_file",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content_type",
                table: "part_file");

            migrationBuilder.DropColumn(
                name: "content_type",
                table: "equipment_file");

            migrationBuilder.AddColumn<int>(
                name: "equipment_statusid",
                table: "equipments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_equipments_equipment_statusid",
                table: "equipments",
                column: "equipment_statusid");

            migrationBuilder.AddForeignKey(
                name: "FK_equipments_equipment_status_equipment_statusid",
                table: "equipments",
                column: "equipment_statusid",
                principalTable: "equipment_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
