using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleGuard.Api.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "NVARCHAR(60)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    FcmToken = table.Column<string>(type: "NVARCHAR(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlate = table.Column<string>(type: "NVARCHAR(60)", nullable: false),
                    Color = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    Brand = table.Column<string>(type: "NVARCHAR(60)", nullable: false),
                    Model = table.Column<string>(type: "NVARCHAR(60)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EmbeddedDeviceId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmbeddedDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbeddedDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbeddedDevice_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmbeddedDevices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommandLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmbeddedDeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Command = table.Column<string>(type: "NVARCHAR(60)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR(60)", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandLogs_EmbeddedDevice_EmbeddedDeviceId",
                        column: x => x.EmbeddedDeviceId,
                        principalTable: "EmbeddedDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommandLogs_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommandLogs_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "Int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Hdop = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EmbeddedDeviceId = table.Column<int>(type: "Int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gps_EmbeddedDevice_EmbeddedDeviceId",
                        column: x => x.EmbeddedDeviceId,
                        principalTable: "EmbeddedDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmbeddedDeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Classification = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    IsSensitivePeriod = table.Column<bool>(type: "BIT", nullable: false),
                    IsUserNearby = table.Column<bool>(type: "BIT", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleEvents_EmbeddedDevice_EmbeddedDeviceId",
                        column: x => x.EmbeddedDeviceId,
                        principalTable: "EmbeddedDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleEvents_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleEvents_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    VehicleEventId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Body = table.Column<string>(type: "NVARCHAR(500)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationLogs_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationLogs_VehicleEvent_VehicleEventId",
                        column: x => x.VehicleEventId,
                        principalTable: "VehicleEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandLogs_EmbeddedDeviceId",
                table: "CommandLogs",
                column: "EmbeddedDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandLogs_UserId",
                table: "CommandLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandLogs_VehicleId",
                table: "CommandLogs",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddedDevice_VehicleId",
                table: "EmbeddedDevices",
                column: "VehicleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddedDevices_UserId",
                table: "EmbeddedDevices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gps_EmbeddedDeviceId",
                table: "Gps",
                column: "EmbeddedDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_UserId",
                table: "NotificationLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_VehicleEventId",
                table: "NotificationLogs",
                column: "VehicleEventId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEvents_EmbeddedDeviceId",
                table: "VehicleEvents",
                column: "EmbeddedDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEvents_OccurredAt",
                table: "VehicleEvents",
                column: "OccurredAt");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEvents_UserId",
                table: "VehicleEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEvents_VehicleId",
                table: "VehicleEvents",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_LicensePlate",
                table: "Vehicles",
                column: "LicensePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandLogs");

            migrationBuilder.DropTable(
                name: "Gps");

            migrationBuilder.DropTable(
                name: "NotificationLogs");

            migrationBuilder.DropTable(
                name: "VehicleEvents");

            migrationBuilder.DropTable(
                name: "EmbeddedDevices");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
