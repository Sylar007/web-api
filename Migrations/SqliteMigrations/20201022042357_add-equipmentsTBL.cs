using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class addequipmentsTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "current_nav_equipment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_id = table.Column<int>(nullable: false),
                    depreciation_st_yrmth = table.Column<string>(nullable: true),
                    depreciation_ed_yrmth = table.Column<string>(nullable: true),
                    depreciation_year = table.Column<int>(nullable: false),
                    depreciation_value = table.Column<decimal>(nullable: false),
                    acc_depn_value = table.Column<decimal>(nullable: false),
                    current_nav = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_current_nav_equipment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_field",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    field_value = table.Column<string>(nullable: true),
                    field_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_field", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_file",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_id = table.Column<int>(nullable: false),
                    media_id = table.Column<int>(nullable: false),
                    file_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_file", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_link",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_id = table.Column<int>(nullable: false),
                    link = table.Column<string>(nullable: true),
                    link_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_link", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_model",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    equipment_type_id = table.Column<int>(nullable: false),
                    process_name = table.Column<string>(nullable: true),
                    model_name = table.Column<string>(nullable: true),
                    model_no = table.Column<string>(nullable: true),
                    mfg_name = table.Column<string>(nullable: true),
                    sales_contact_name = table.Column<string>(nullable: true),
                    sales_contact_no = table.Column<string>(nullable: true),
                    support_contact_name = table.Column<string>(nullable: true),
                    support_contact_no = table.Column<string>(nullable: true),
                    remark = table.Column<string>(nullable: true),
                    is_deleted = table.Column<sbyte>(nullable: false),
                    dt_deleted = table.Column<DateTime>(nullable: true),
                    dt_created = table.Column<DateTime>(nullable: true),
                    dt_modified = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<int>(nullable: true),
                    modified_by = table.Column<int>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_model", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_model_field",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_model_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    field_value = table.Column<string>(nullable: true),
                    field_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_model_field", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_model_file",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_model_id = table.Column<int>(nullable: false),
                    media_id = table.Column<int>(nullable: false),
                    file_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_model_file", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_model_link",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_model_id = table.Column<int>(nullable: false),
                    link = table.Column<string>(nullable: true),
                    link_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_model_link", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_model_part",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_model_id = table.Column<int>(nullable: false),
                    part_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_model_part", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_model_part_model",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_model_id = table.Column<int>(nullable: false),
                    part_model_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_model_part_model", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_part",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_id = table.Column<int>(nullable: false),
                    part_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_part", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_status",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    dt_created = table.Column<DateTime>(nullable: true),
                    dt_modified = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<int>(nullable: true),
                    modified_by = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipment_no = table.Column<string>(nullable: true),
                    equipment_model_id = table.Column<int>(nullable: false),
                    serial_no = table.Column<string>(nullable: true),
                    mfg_year = table.Column<int>(nullable: true),
                    sales_contact_name = table.Column<string>(nullable: true),
                    sales_contact_no = table.Column<string>(nullable: true),
                    support_contact_name = table.Column<string>(nullable: true),
                    support_contact_no = table.Column<string>(nullable: true),
                    dt_acquisition = table.Column<DateTime>(nullable: false),
                    dt_warranty_exp = table.Column<DateTime>(nullable: true),
                    dt_site_delivery = table.Column<DateTime>(nullable: false),
                    dt_installation = table.Column<DateTime>(nullable: false),
                    dt_commissioning = table.Column<DateTime>(nullable: false),
                    cert_no = table.Column<string>(nullable: true),
                    dt_cert = table.Column<DateTime>(nullable: true),
                    remark = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    longitude = table.Column<string>(nullable: true),
                    latitude = table.Column<string>(nullable: true),
                    horsepower = table.Column<decimal>(nullable: false),
                    equipment_status_id = table.Column<int>(nullable: false),
                    equipment_statusid = table.Column<int>(nullable: true),
                    media_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<sbyte>(nullable: false),
                    dt_deleted = table.Column<DateTime>(nullable: true),
                    dt_created = table.Column<DateTime>(nullable: true),
                    dt_modified = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<int>(nullable: true),
                    modified_by = table.Column<int>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_equipments_equipment_status_equipment_statusid",
                        column: x => x.equipment_statusid,
                        principalTable: "equipment_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_equipments_equipment_statusid",
                table: "equipments",
                column: "equipment_statusid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "current_nav_equipment");

            migrationBuilder.DropTable(
                name: "equipment_field");

            migrationBuilder.DropTable(
                name: "equipment_file");

            migrationBuilder.DropTable(
                name: "equipment_link");

            migrationBuilder.DropTable(
                name: "equipment_model");

            migrationBuilder.DropTable(
                name: "equipment_model_field");

            migrationBuilder.DropTable(
                name: "equipment_model_file");

            migrationBuilder.DropTable(
                name: "equipment_model_link");

            migrationBuilder.DropTable(
                name: "equipment_model_part");

            migrationBuilder.DropTable(
                name: "equipment_model_part_model");

            migrationBuilder.DropTable(
                name: "equipment_part");

            migrationBuilder.DropTable(
                name: "equipment_type");

            migrationBuilder.DropTable(
                name: "equipments");

            migrationBuilder.DropTable(
                name: "equipment_status");
        }
    }
}
