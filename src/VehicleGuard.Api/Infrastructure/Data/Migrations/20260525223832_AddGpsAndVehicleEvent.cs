using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleGuard.Api.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGpsAndVehicleEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleEvents_User_UserId",
                table: "VehicleEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleEvents_Vehicle_VehicleId",
                table: "VehicleEvents");

            migrationBuilder.DropIndex(
                name: "IX_VehicleEvents_VehicleId",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "IsUserNearby",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "VehicleEvents");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "VehicleEvents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "EmbeddedDeviceId",
                table: "VehicleEvents",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "GpsId",
                table: "VehicleEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Gps",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEvents_GpsId",
                table: "VehicleEvents",
                column: "GpsId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleEvents_Gps_GpsId",
                table: "VehicleEvents",
                column: "GpsId",
                principalTable: "Gps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleEvents_Users_UserId",
                table: "VehicleEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleEvents_Gps_GpsId",
                table: "VehicleEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleEvents_Users_UserId",
                table: "VehicleEvents");

            migrationBuilder.DropIndex(
                name: "IX_VehicleEvents_GpsId",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "GpsId",
                table: "VehicleEvents");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "VehicleEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmbeddedDeviceId",
                table: "VehicleEvents",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VehicleEvents",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<bool>(
                name: "IsUserNearby",
                table: "VehicleEvents",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "VehicleEvents",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "VehicleEvents",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "VehicleEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Gps",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEvents_VehicleId",
                table: "VehicleEvents",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleEvents_User_UserId",
                table: "VehicleEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleEvents_Vehicle_VehicleId",
                table: "VehicleEvents",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
