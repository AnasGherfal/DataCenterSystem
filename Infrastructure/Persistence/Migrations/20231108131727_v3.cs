using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 198, DateTimeKind.Utc).AddTicks(1339),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 324, DateTimeKind.Utc).AddTicks(2588));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 198, DateTimeKind.Utc).AddTicks(1101),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 324, DateTimeKind.Utc).AddTicks(2246));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 198, DateTimeKind.Utc).AddTicks(231),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 324, DateTimeKind.Utc).AddTicks(1019));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 198, DateTimeKind.Utc).AddTicks(21),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 324, DateTimeKind.Utc).AddTicks(719));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(7794),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(7393));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(7576),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(7074));

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractDate",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ContractNumber",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(6657),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(5661));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(6468),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(5399));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(5303),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(3852));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(5013),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(3447));

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveFrom",
                table: "Representatives",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveTo",
                table: "Representatives",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RepresentativeType",
                table: "Representatives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(9025),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(9245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(8774),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(8950));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(3942),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(2017));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(3723),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(1729));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Admins",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(2452),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(162));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Admins",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(2103),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 322, DateTimeKind.Utc).AddTicks(9710));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractDate",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ContractNumber",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ActiveFrom",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "ActiveTo",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "RepresentativeType",
                table: "Representatives");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 324, DateTimeKind.Utc).AddTicks(2588),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 198, DateTimeKind.Utc).AddTicks(1339));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Visits",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 324, DateTimeKind.Utc).AddTicks(2246),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 198, DateTimeKind.Utc).AddTicks(1101));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 324, DateTimeKind.Utc).AddTicks(1019),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 198, DateTimeKind.Utc).AddTicks(231));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TimeShifts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 324, DateTimeKind.Utc).AddTicks(719),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 198, DateTimeKind.Utc).AddTicks(21));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(7393),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(7794));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Subscriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(7074),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(7576));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(5661),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(6657));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Services",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(5399),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(6468));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(3852),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(5303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Representatives",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(3447),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(5013));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(9245),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(9025));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Invoices",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(8950),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(8774));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(2017),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(3942));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(1729),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(3723));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Admins",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 323, DateTimeKind.Utc).AddTicks(162),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(2452));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Admins",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 2, 8, 18, 6, 322, DateTimeKind.Utc).AddTicks(9710),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 11, 8, 13, 17, 27, 197, DateTimeKind.Utc).AddTicks(2103));
        }
    }
}
