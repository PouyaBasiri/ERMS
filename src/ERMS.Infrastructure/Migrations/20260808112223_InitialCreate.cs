using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LAST_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
