using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class Parts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estimated_nav",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_id = table.Column<int>(nullable: false),
                    dt_installation = table.Column<DateTime>(nullable: false),
                    dt_operation = table.Column<DateTime>(nullable: false),
                    dt_purchase = table.Column<DateTime>(nullable: false),
                    dt_obsolete = table.Column<DateTime>(nullable: false),
                    purchase_value = table.Column<decimal>(nullable: false),
                    dt_invoice = table.Column<DateTime>(nullable: true),
                    invoice_no = table.Column<string>(nullable: true),
                    is_deleted = table.Column<int>(nullable: false),
                    dt_created = table.Column<DateTime>(nullable: true),
                    dt_modified = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<int>(nullable: true),
                    modified_by = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estimated_nav", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    file_name = table.Column<string>(nullable: true),
                    dt_created = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "part_model",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    part_type_id = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    model_name = table.Column<string>(nullable: true),
                    model_no = table.Column<string>(nullable: true),
                    mfg_name = table.Column<string>(nullable: true),
                    sales_contact_name = table.Column<string>(nullable: true),
                    sales_contact_no = table.Column<string>(nullable: true),
                    support_contact_name = table.Column<string>(nullable: true),
                    support_contact_no = table.Column<string>(nullable: true),
                    remark = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_part_model", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "parts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    part_no = table.Column<string>(nullable: true),
                    part_model_id = table.Column<int>(nullable: false),
                    serial_no = table.Column<string>(nullable: true),
                    mfg_year = table.Column<int>(nullable: true),
                    dt_acquisition = table.Column<DateTime>(nullable: false),
                    dt_warranty_exp = table.Column<DateTime>(nullable: true),
                    dt_installation = table.Column<DateTime>(nullable: true),
                    sales_contact_name = table.Column<string>(nullable: true),
                    sales_contact_no = table.Column<string>(nullable: true),
                    support_contact_name = table.Column<string>(nullable: true),
                    support_contact_no = table.Column<string>(nullable: true),
                    cert_no = table.Column<string>(nullable: true),
                    dt_cert = table.Column<DateTime>(nullable: true),
                    remark = table.Column<string>(nullable: true),
                    media_id = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_parts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "policies",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_model_id = table.Column<int>(nullable: false),
                    life_span = table.Column<int>(nullable: false),
                    depreciation_rate = table.Column<decimal>(nullable: false),
                    is_deleted = table.Column<int>(nullable: false),
                    dt_created = table.Column<DateTime>(nullable: true),
                    dt_modified = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<int>(nullable: true),
                    modified_by = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policies", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estimated_nav");

            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropTable(
                name: "part_model");

            migrationBuilder.DropTable(
                name: "parts");

            migrationBuilder.DropTable(
                name: "policies");
        }
    }
}
