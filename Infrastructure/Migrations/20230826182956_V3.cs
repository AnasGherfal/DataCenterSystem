using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(7318),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(7067));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(7008),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(6706));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(5782),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(5483));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(5536),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(5212));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(2266),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(1863));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(1955),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(1531));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(701),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(432),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(9897));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 951, DateTimeKind.Utc).AddTicks(9072),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(8485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 951, DateTimeKind.Utc).AddTicks(8732),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(8137));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(4237),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(3724));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(3921),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(3411));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 951, DateTimeKind.Utc).AddTicks(7324),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(6674));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 951, DateTimeKind.Utc).AddTicks(6985),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(6300));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(7067),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(7318));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(6706),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(7008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(5483),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(5782));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(5212),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(5536));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(1863),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(2266));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(1531),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(1955));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(188),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(701));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(9897),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(432));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(8485),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 951, DateTimeKind.Utc).AddTicks(9072));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(8137),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 951, DateTimeKind.Utc).AddTicks(8732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(3724),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(4237));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 52, DateTimeKind.Utc).AddTicks(3411),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 952, DateTimeKind.Utc).AddTicks(3921));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(6674),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 951, DateTimeKind.Utc).AddTicks(7324));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 26, 18, 28, 28, 51, DateTimeKind.Utc).AddTicks(6300),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 8, 26, 18, 29, 55, 951, DateTimeKind.Utc).AddTicks(6985));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
