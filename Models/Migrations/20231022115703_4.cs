using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Category_CategoryID",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Governance",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Service");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Service",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 57, 3, 540, DateTimeKind.Local).AddTicks(2829),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 643, DateTimeKind.Local).AddTicks(4661));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "PromoCode",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 57, 3, 539, DateTimeKind.Local).AddTicks(9623),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 642, DateTimeKind.Local).AddTicks(8285));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "BookingDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 57, 3, 538, DateTimeKind.Local).AddTicks(7593),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 640, DateTimeKind.Local).AddTicks(9898));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 57, 3, 538, DateTimeKind.Local).AddTicks(5105),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 640, DateTimeKind.Local).AddTicks(7419));

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Category_CategoryID",
                table: "Service",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Category_CategoryID",
                table: "Service");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Service",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Governance",
                table: "Service",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Service",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 643, DateTimeKind.Local).AddTicks(4661),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 57, 3, 540, DateTimeKind.Local).AddTicks(2829));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "PromoCode",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 642, DateTimeKind.Local).AddTicks(8285),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 57, 3, 539, DateTimeKind.Local).AddTicks(9623));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "BookingDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 640, DateTimeKind.Local).AddTicks(9898),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 57, 3, 538, DateTimeKind.Local).AddTicks(7593));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 14, 32, 23, 640, DateTimeKind.Local).AddTicks(7419),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 22, 14, 57, 3, 538, DateTimeKind.Local).AddTicks(5105));

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Category_CategoryID",
                table: "Service",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
