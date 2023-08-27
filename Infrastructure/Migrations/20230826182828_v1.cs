using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "TimeShifts");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(7067),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(8886));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(6706),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(8632));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(5483),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(7665));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(5212),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(7448));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(1863),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(4973));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(1531),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(4723));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(188),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(3736));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(9897),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(3533));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(8485),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(2443));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(8137),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(2172));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(3724),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(6347));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(3411),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(6107));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(6674),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(1068));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(6300),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(757));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(8886),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(7067));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(8632),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(6706));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(7665),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(5483));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(7448),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(5212));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(4973),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(1863));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(4723),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(1531));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(3736),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(3533),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(9897));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(2443),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(8485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(2172),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(8137));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(6347),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(3724));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(6107),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(3411));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(1068),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(6674));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 24, 0, 889, DateTimeKind.Utc).AddTicks(757),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(6300));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
