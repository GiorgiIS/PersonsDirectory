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
                values: new object[,]
                {
                    { 1, new DateTime(1994, 12, 15, 8, 38, 8, 423, DateTimeKind.Local).AddTicks(6699), 1, new DateTime(2019, 12, 15, 4, 38, 8, 423, DateTimeKind.Utc).AddTicks(4620), null, "Kaka", 1, "NO_IMAGE", "Kuku", "12345678911", new DateTime(2019, 12, 15, 4, 38, 8, 423, DateTimeKind.Utc).AddTicks(5279) },
                    { 2, new DateTime(1994, 12, 15, 8, 38, 8, 424, DateTimeKind.Local).AddTicks(7466), 1, new DateTime(2019, 12, 15, 4, 38, 8, 424, DateTimeKind.Utc).AddTicks(7432), null, "Paolo", 1, "NO_IMAGE", "Maldini", "06345678911", new DateTime(2019, 12, 15, 4, 38, 8, 424, DateTimeKind.Utc).AddTicks(7443) },
                    { 3, new DateTime(1991, 12, 15, 8, 38, 8, 424, DateTimeKind.Local).AddTicks(7533), 1, new DateTime(2019, 12, 15, 4, 38, 8, 424, DateTimeKind.Utc).AddTicks(7531), null, "არაგორნ", 1, "NO_IMAGE", "ელენდიელი", "01457876911", new DateTime(2019, 12, 15, 4, 38, 8, 424, DateTimeKind.Utc).AddTicks(7532) },
                    { 4, new DateTime(1993, 12, 15, 8, 38, 8, 424, DateTimeKind.Local).AddTicks(7539), 1, new DateTime(2019, 12, 15, 4, 38, 8, 424, DateTimeKind.Utc).AddTicks(7537), null, "ვიქტორ", 1, "NO_IMAGE", "ჰიუგო", "11566378911", new DateTime(2019, 12, 15, 4, 38, 8, 424, DateTimeKind.Utc).AddTicks(7538) },
                    { 5, new DateTime(1996, 12, 15, 8, 38, 8, 424, DateTimeKind.Local).AddTicks(7543), 1, new DateTime(2019, 12, 15, 4, 38, 8, 424, DateTimeKind.Utc).AddTicks(7541), null, "ვარდან", 1, "NO_IMAGE", "მამეკონიანი", "12366278911", new DateTime(2019, 12, 15, 4, 38, 8, 424, DateTimeKind.Utc).AddTicks(7542) }
                });

            migrationBuilder.InsertData(
                table: "PersonConnections",
                columns: new[] { "Id", "ConnectionTypeId", "CreatedAt", "DeletedAt", "FirstPersonId", "SecondPersonId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(1417), null, 1, 2, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(1421) },
                    { 4, 2, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(2749), null, 2, 1, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(2749) },
                    { 2, 1, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(2719), null, 1, 3, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(2722) },
                    { 5, 1, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(2751), null, 2, 3, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(2751) },
                    { 6, 1, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(2755), null, 3, 1, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(2756) },
                    { 3, 2, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(2746), null, 1, 4, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(2747) }
                });

            migrationBuilder.InsertData(
                table: "PersonPhoneNumber",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Number", "PersonId", "PhoneNumberTypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 15, 4, 38, 8, 424, DateTimeKind.Utc).AddTicks(8793), null, "111111111", 1, 1, new DateTime(2019, 12, 15, 4, 38, 8, 424, DateTimeKind.Utc).AddTicks(8798) },
                    { 2, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(96), null, "222222222", 1, 2, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(100) },
                    { 3, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(123), null, "333333333", 2, 2, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(124) },
                    { 4, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(125), null, "55555555", 2, 1, new DateTime(2019, 12, 15, 4, 38, 8, 425, DateTimeKind.Utc).AddTicks(126) }
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
