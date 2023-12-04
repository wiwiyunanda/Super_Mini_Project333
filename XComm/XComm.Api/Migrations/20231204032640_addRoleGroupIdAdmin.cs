using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XComm.Api.Migrations
{
    /// <inheritdoc />
    public partial class addRoleGroupIdAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7840));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7842));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7843));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7844));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7845));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "Role" },
                values: new object[] { new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7845), "Galleries" });

            migrationBuilder.InsertData(
                table: "AuthorizationGroups",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Role", "RoleGroupId" },
                values: new object[] { 7L, "admin", new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7846), null, null, "Orders", 2L });

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin",
                columns: new[] { "CreatedDate", "RoleGroupId" },
                values: new object[] { new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7592), 2L });

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "user1",
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7621));

            migrationBuilder.UpdateData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7811));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2500));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2502));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2503));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2503));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2504));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "Role" },
                values: new object[] { new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2505), "Orders" });

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin",
                columns: new[] { "CreatedDate", "RoleGroupId" },
                values: new object[] { new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2361), 1L });

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "user1",
                column: "CreatedDate",
                value: new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2374));

            migrationBuilder.UpdateData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2487));

            migrationBuilder.UpdateData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2488));
        }
    }
}
