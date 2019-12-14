using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBCPersonsDirectory.Repository.EF.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    GenderId = table.Column<int>(nullable: true),
                    PrivateNumber = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Citys_CityId",
                        column: x => x.CityId,
                        principalTable: "Citys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonConnections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    FirstPersonId = table.Column<int>(nullable: true),
                    SecondPersonId = table.Column<int>(nullable: true),
                    ConnectionTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonConnections_ConnectionType_ConnectionTypeId",
                        column: x => x.ConnectionTypeId,
                        principalTable: "ConnectionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonConnections_Persons_FirstPersonId",
                        column: x => x.FirstPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonConnections_Persons_SecondPersonId",
                        column: x => x.SecondPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonPhoneNumber",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    PhoneNumberTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonPhoneNumber_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonPhoneNumber_PhoneNumberTypes_PhoneNumberTypeId",
                        column: x => x.PhoneNumberTypeId,
                        principalTable: "PhoneNumberTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Citys",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tbilisi" },
                    { 2, "Batumi" },
                    { 3, "Kutaisi" },
                    { 4, "Foti" }
                });

            migrationBuilder.InsertData(
                table: "ConnectionType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "College" },
                    { 2, "Familiar" },
                    { 3, "Relative" },
                    { 4, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "PhoneNumberTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mobile" },
                    { 2, "Office" },
                    { 3, "Home" }
                });

            migrationBuilder.InsertData(
                table: "PersonPhoneNumber",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Number", "PersonId", "PhoneNumberTypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(2504), null, "111111111", null, 1, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(2510) },
                    { 4, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(4419), null, "55555555", null, 1, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(4420) },
                    { 2, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(4382), null, "222222222", null, 2, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(4386) },
                    { 3, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(4416), null, "333333333", null, 2, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(4417) }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "BirthDate", "CityId", "CreatedAt", "DeletedAt", "FirstName", "GenderId", "ImageUrl", "LastName", "PrivateNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1994, 12, 14, 16, 53, 5, 292, DateTimeKind.Local).AddTicks(9711), 1, new DateTime(2019, 12, 14, 12, 53, 5, 292, DateTimeKind.Utc).AddTicks(7022), null, "Kaka", 1, "NO_IMAGE", "Kuku", "12345678911", new DateTime(2019, 12, 14, 12, 53, 5, 292, DateTimeKind.Utc).AddTicks(7722) },
                    { 2, new DateTime(1994, 12, 14, 16, 53, 5, 294, DateTimeKind.Local).AddTicks(1050), 1, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(1014), null, "Paolo", 1, "NO_IMAGE", "Maldini", "06345678911", new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(1028) }
                });

            migrationBuilder.InsertData(
                table: "PersonConnections",
                columns: new[] { "Id", "ConnectionTypeId", "CreatedAt", "DeletedAt", "FirstPersonId", "SecondPersonId", "UpdatedAt" },
                values: new object[] { 1, 1, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(5792), null, 1, 2, new DateTime(2019, 12, 14, 12, 53, 5, 294, DateTimeKind.Utc).AddTicks(5796) });

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_ConnectionTypeId",
                table: "PersonConnections",
                column: "ConnectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_FirstPersonId",
                table: "PersonConnections",
                column: "FirstPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_SecondPersonId",
                table: "PersonConnections",
                column: "SecondPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhoneNumber_PersonId",
                table: "PersonPhoneNumber",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhoneNumber_PhoneNumberTypeId",
                table: "PersonPhoneNumber",
                column: "PhoneNumberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityId",
                table: "Persons",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderId",
                table: "Persons",
                column: "GenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonConnections");

            migrationBuilder.DropTable(
                name: "PersonPhoneNumber");

            migrationBuilder.DropTable(
                name: "ConnectionType");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "PhoneNumberTypes");

            migrationBuilder.DropTable(
                name: "Citys");

            migrationBuilder.DropTable(
                name: "Gender");
        }
    }
}
