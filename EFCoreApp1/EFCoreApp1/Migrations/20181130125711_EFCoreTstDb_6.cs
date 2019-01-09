using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreApp1.Migrations
{
    public partial class EFCoreTstDb_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Department",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Department",
                nullable: false,
                defaultValue: new DateTime(2018, 11, 30, 14, 57, 11, 84, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 11, 30, 14, 40, 34, 882, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Department",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Department",
                nullable: false,
                defaultValue: new DateTime(2018, 11, 30, 14, 40, 34, 882, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 11, 30, 14, 57, 11, 84, DateTimeKind.Local));
        }
    }
}
