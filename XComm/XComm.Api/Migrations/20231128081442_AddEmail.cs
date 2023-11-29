using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XComm.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "MasterAccount",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "winkawinoy@gmail.com");

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin",
                columns: new[] { "CreatedDate", "Email" },
                values: new object[] { new DateTime(2023, 11, 28, 15, 14, 42, 722, DateTimeKind.Local).AddTicks(8597), "winkawinoy@gmail.com" });

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "user1",
                columns: new[] { "CreatedDate", "Email" },
                values: new object[] { new DateTime(2023, 11, 28, 15, 14, 42, 722, DateTimeKind.Local).AddTicks(8612), "winkawinoy@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "MasterAccount");

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin",
                column: "CreatedDate",
                value: new DateTime(2023, 11, 28, 14, 45, 31, 472, DateTimeKind.Local).AddTicks(3521));

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "user1",
                column: "CreatedDate",
                value: new DateTime(2023, 11, 28, 14, 45, 31, 472, DateTimeKind.Local).AddTicks(3543));
        }
    }
}
