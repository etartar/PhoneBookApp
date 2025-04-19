using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBookApp.Services.Contact.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_Person_ContactInformation_Entities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "persons",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                surname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                company_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                deleted_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_persons", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "contact_informations",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                person_id = table.Column<Guid>(type: "uuid", nullable: false),
                information_type = table.Column<int>(type: "integer", nullable: false),
                information_content = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_contact_informations", x => x.id);
                table.ForeignKey(
                    name: "fk_contact_informations_persons_person_id",
                    column: x => x.person_id,
                    principalTable: "persons",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "ix_contact_informations_person_id",
            table: "contact_informations",
            column: "person_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "contact_informations");

        migrationBuilder.DropTable(
            name: "persons");
    }
}
