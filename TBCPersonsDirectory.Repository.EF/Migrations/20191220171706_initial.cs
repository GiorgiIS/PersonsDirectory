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
                    PersonId = table.Column<int>(nullable: false),
                    Number = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumberTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonPhoneNumber_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPhoneNumber_PhoneNumberTypes_PhoneNumberTypeId",
                        column: x => x.PhoneNumberTypeId,
                        principalTable: "PhoneNumberTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Persons",
                columns: new[] { "Id", "BirthDate", "CityId", "CreatedAt", "DeletedAt", "FirstName", "GenderId", "ImageUrl", "LastName", "PrivateNumber", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1994, 12, 20, 21, 17, 6, 126, DateTimeKind.Local).AddTicks(4443), 1, new DateTime(2019, 12, 20, 17, 17, 6, 126, DateTimeKind.Utc).AddTicks(541), null, "Kaka", 1, "NO_IMAGE", "Kuku", "12345678911", new DateTime(2019, 12, 20, 17, 17, 6, 126, DateTimeKind.Utc).AddTicks(2318) });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "BirthDate", "CityId", "CreatedAt", "DeletedAt", "FirstName", "GenderId", "ImageUrl", "LastName", "PrivateNumber", "UpdatedAt" },
                values: new object[] { 2, new DateTime(1994, 12, 20, 21, 17, 6, 127, DateTimeKind.Local).AddTicks(7653), 1, new DateTime(2019, 12, 20, 17, 17, 6, 127, DateTimeKind.Utc).AddTicks(7617), null, "Paolo", 1, "NO_IMAGE", "Maldini", "06345678911", new DateTime(2019, 12, 20, 17, 17, 6, 127, DateTimeKind.Utc).AddTicks(7631) });

            migrationBuilder.InsertData(
                table: "PersonConnections",
                columns: new[] { "Id", "ConnectionTypeId", "CreatedAt", "DeletedAt", "FirstPersonId", "SecondPersonId", "UpdatedAt" },
                values: new object[] { 1, 1, new DateTime(2019, 12, 20, 17, 17, 6, 128, DateTimeKind.Utc).AddTicks(3515), null, 1, 2, new DateTime(2019, 12, 20, 17, 17, 6, 128, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.InsertData(
                table: "PersonPhoneNumber",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Number", "PersonId", "PhoneNumberTypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 20, 17, 17, 6, 128, DateTimeKind.Utc).AddTicks(5), null, "111111111", 1, 1, new DateTime(2019, 12, 20, 17, 17, 6, 128, DateTimeKind.Utc).AddTicks(9) },
                    { 2, new DateTime(2019, 12, 20, 17, 17, 6, 128, DateTimeKind.Utc).AddTicks(1964), null, "222222222", 1, 2, new DateTime(2019, 12, 20, 17, 17, 6, 128, DateTimeKind.Utc).AddTicks(1968) },
                    { 3, new DateTime(2019, 12, 20, 17, 17, 6, 128, DateTimeKind.Utc).AddTicks(1994), null, "333333333", 2, 2, new DateTime(2019, 12, 20, 17, 17, 6, 128, DateTimeKind.Utc).AddTicks(1995) },
                    { 4, new DateTime(2019, 12, 20, 17, 17, 6, 128, DateTimeKind.Utc).AddTicks(1997), null, "55555555", 2, 1, new DateTime(2019, 12, 20, 17, 17, 6, 128, DateTimeKind.Utc).AddTicks(1998) }
                });

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
