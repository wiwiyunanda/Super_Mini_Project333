using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XComm.Api.Migrations
{
    /// <inheritdoc />
    public partial class RoleGroupAuthorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attempt",
                table: "MasterAccount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Otp",
                table: "MasterAccount",
                type: "char(6)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RoleGroupId",
                table: "MasterAccount",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateTable(
                name: "RoleGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizationGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleGroupId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorizationGroups_RoleGroups_RoleGroupId",
                        column: x => x.RoleGroupId,
                        principalTable: "RoleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin",
                columns: new[] { "Attempt", "CreatedDate", "Otp" },
                values: new object[] { 0, new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8835), null });

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "user1",
                columns: new[] { "Attempt", "CreatedDate", "Otp" },
                values: new object[] { 0, new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8851), null });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "GroupName", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { 1L, "admin", new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8974), "Customer", null, null },
                    { 2L, "admin", new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8975), "Kasir", null, null }
                });

            migrationBuilder.InsertData(
                table: "AuthorizationGroups",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Role", "RoleGroupId" },
                values: new object[,]
                {
                    { 1L, "admin", new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8997), null, null, "Products", 1L },
                    { 2L, "admin", new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8999), null, null, "Orders", 1L },
                    { 3L, "admin", new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8999), null, null, "Categories", 2L },
                    { 4L, "admin", new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(9000), null, null, "Variants", 2L },
                    { 5L, "admin", new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(9001), null, null, "Products", 2L },
                    { 6L, "admin", new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(9001), null, null, "Orders", 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterAccount_RoleGroupId",
                table: "MasterAccount",
                column: "RoleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizationGroups_RoleGroupId",
                table: "AuthorizationGroups",
                column: "RoleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroups_GroupName",
                table: "RoleGroups",
                column: "GroupName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterAccount_RoleGroups_RoleGroupId",
                table: "MasterAccount",
                column: "RoleGroupId",
                principalTable: "RoleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterAccount_RoleGroups_RoleGroupId",
                table: "MasterAccount");

            migrationBuilder.DropTable(
                name: "AuthorizationGroups");

            migrationBuilder.DropTable(
                name: "RoleGroups");

            migrationBuilder.DropIndex(
                name: "IX_MasterAccount_RoleGroupId",
                table: "MasterAccount");

            migrationBuilder.DropColumn(
                name: "Attempt",
                table: "MasterAccount");

            migrationBuilder.DropColumn(
                name: "Otp",
                table: "MasterAccount");

            migrationBuilder.DropColumn(
                name: "RoleGroupId",
                table: "MasterAccount");

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin",
                column: "CreatedDate",
                value: new DateTime(2023, 11, 28, 15, 14, 42, 722, DateTimeKind.Local).AddTicks(8597));

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "user1",
                column: "CreatedDate",
                value: new DateTime(2023, 11, 28, 15, 14, 42, 722, DateTimeKind.Local).AddTicks(8612));
        }
    }
}
