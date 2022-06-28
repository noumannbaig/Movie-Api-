using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Data.Migrations
{
    public partial class Authentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ReleaseDate", "Updatedat" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 35, 26, 159, DateTimeKind.Local).AddTicks(926), new DateTime(2022, 6, 27, 13, 35, 26, 162, DateTimeKind.Local).AddTicks(2524) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ReleaseDate", "Updatedat" },
                values: new object[] { new DateTime(2022, 6, 27, 10, 11, 24, 821, DateTimeKind.Local).AddTicks(4278), new DateTime(2022, 6, 27, 10, 11, 24, 822, DateTimeKind.Local).AddTicks(5012) });
        }
    }
}
