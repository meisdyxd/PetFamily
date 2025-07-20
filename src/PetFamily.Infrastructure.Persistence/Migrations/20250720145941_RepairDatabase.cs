using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RepairDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "species",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_species", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    years_employee_exp = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    name = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: true),
                    surname = table.Column<string>(type: "text", nullable: false),
                    telephone_number = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    delete_by_cascade = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    requisits = table.Column<string>(type: "jsonb", nullable: true),
                    social_networks = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "breeds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    species_ids = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breed", x => x.id);
                    table.ForeignKey(
                        name: "FK_breeds_species_species_ids",
                        column: x => x.species_ids,
                        principalTable: "species",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    moniker = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    coloration = table.Column<string>(type: "text", nullable: false),
                    health_info = table.Column<string>(type: "text", nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    height = table.Column<double>(type: "double precision", nullable: false),
                    is_castrated = table.Column<bool>(type: "boolean", nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    city = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false),
                    flat = table.Column<string>(type: "text", nullable: false),
                    region = table.Column<string>(type: "text", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    telephone_number = table.Column<string>(type: "text", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    delete_by_cascade = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    requisits = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet", x => x.id);
                    table.ForeignKey(
                        name: "FK_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_breeds_species_ids",
                table: "breeds",
                column: "species_ids");

            migrationBuilder.CreateIndex(
                name: "IX_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "breeds");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "species");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}
