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
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citys", x => x.Id);
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
                    Gender = table.Column<int>(nullable: true),
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
                });

            migrationBuilder.InsertData(
                table: "Citys",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 14, 10, 43, 47, 195, DateTimeKind.Utc).AddTicks(3977), null, "Tbilisi", new DateTime(2019, 12, 14, 10, 43, 47, 195, DateTimeKind.Utc).AddTicks(4900) },
                    { 2, new DateTime(2019, 12, 14, 10, 43, 47, 195, DateTimeKind.Utc).AddTicks(6753), null, "Batumi", new DateTime(2019, 12, 14, 10, 43, 47, 195, DateTimeKind.Utc).AddTicks(6765) },
                    { 3, new DateTime(2019, 12, 14, 10, 43, 47, 195, DateTimeKind.Utc).AddTicks(6833), null, "Kutaisi", new DateTime(2019, 12, 14, 10, 43, 47, 195, DateTimeKind.Utc).AddTicks(6834) },
                    { 4, new DateTime(2019, 12, 14, 10, 43, 47, 195, DateTimeKind.Utc).AddTicks(6836), null, "Foti", new DateTime(2019, 12, 14, 10, 43, 47, 195, DateTimeKind.Utc).AddTicks(6837) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityId",
                table: "Persons",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Citys");
        }
    }
}
