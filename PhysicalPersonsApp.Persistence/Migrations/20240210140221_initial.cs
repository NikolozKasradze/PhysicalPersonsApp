using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhysicalPersonsApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PersonConnectionTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonConnectionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PhoneType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonalN = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Persons_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonConnections",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionTypeId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    ConnectedPersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonConnections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonConnections_PersonConnectionTypes_ConnectionTypeId",
                        column: x => x.ConnectionTypeId,
                        principalTable: "PersonConnectionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonConnections_Persons_ConnectedPersonId",
                        column: x => x.ConnectedPersonId,
                        principalTable: "Persons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PersonConnections_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneTypeId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_PhoneType_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalTable: "PhoneType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "თბილისი" },
                    { 2, "ბათუმი" },
                    { 3, "ქუთაისი" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "ქალი" },
                    { 2, "კაცი" }
                });

            migrationBuilder.InsertData(
                table: "PersonConnectionTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "კოლეგა" },
                    { 2, "ნაცნობი" },
                    { 3, "ნათესავი" },
                    { 4, "სხვა" }
                });

            migrationBuilder.InsertData(
                table: "PhoneType",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "მობილური" },
                    { 2, "ოფისის" },
                    { 3, "სახლის" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_ConnectedPersonId",
                table: "PersonConnections",
                column: "ConnectedPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_ConnectionTypeId",
                table: "PersonConnections",
                column: "ConnectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_PersonId",
                table: "PersonConnections",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityId",
                table: "Persons",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderId",
                table: "Persons",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_PersonId",
                table: "PhoneNumbers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_PhoneTypeId",
                table: "PhoneNumbers",
                column: "PhoneTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonConnections");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "PersonConnectionTypes");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "PhoneType");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
