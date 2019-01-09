using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreApp1.Migrations
{
    public partial class EFCoreTstDb_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Department",
                nullable: false,
                defaultValue: new DateTime(2018, 11, 30, 14, 34, 48, 8, DateTimeKind.Local),
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Department",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 11, 30, 14, 34, 48, 8, DateTimeKind.Local));
        }
    }
}
