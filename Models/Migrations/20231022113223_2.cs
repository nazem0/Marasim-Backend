using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentUrl",
                table: "ServiceAttachment",
                newName: "Resource");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 643, DateTimeKind.Local).AddTicks(4661),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 13, 15, 50, 267, DateTimeKind.Local).AddTicks(6620));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "PromoCode",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 642, DateTimeKind.Local).AddTicks(8285),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 13, 15, 50, 267, DateTimeKind.Local).AddTicks(3477));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "BookingDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 640, DateTimeKind.Local).AddTicks(9898),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 13, 15, 50, 266, DateTimeKind.Local).AddTicks(1026));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 640, DateTimeKind.Local).AddTicks(7419),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 13, 15, 50, 265, DateTimeKind.Local).AddTicks(8715));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Resource",
                table: "ServiceAttachment",
                newName: "AttachmentUrl");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 13, 15, 50, 267, DateTimeKind.Local).AddTicks(6620),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 643, DateTimeKind.Local).AddTicks(4661));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "PromoCode",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 13, 15, 50, 267, DateTimeKind.Local).AddTicks(3477),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 642, DateTimeKind.Local).AddTicks(8285));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "BookingDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 13, 15, 50, 266, DateTimeKind.Local).AddTicks(1026),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 640, DateTimeKind.Local).AddTicks(9898));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 13, 15, 50, 265, DateTimeKind.Local).AddTicks(8715),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 640, DateTimeKind.Local).AddTicks(7419));
        }
    }
}
