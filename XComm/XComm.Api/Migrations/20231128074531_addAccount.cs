using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XComm.Api.Migrations
{
    /// <inheritdoc />
    public partial class addAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MasterAccount",
                columns: new[] { "UserName", "Active", "CreatedBy", "CreatedDate", "FirstName", "Id", "LastName", "ModifiedBy", "ModifiedDate", "Password" },
                values: new object[,]
                {
                    { "admin", true, "admin", new DateTime(2023, 11, 28, 14, 45, 31, 472, DateTimeKind.Local).AddTicks(3521), "Super", 1L, "Admin", null, null, "ac9689e2272427085e35b9d3e3e8bed88cb3434828b43b86fc0596cad4c6e270" },
                    { "user1", true, "admin", new DateTime(2023, 11, 28, 14, 45, 31, 472, DateTimeKind.Local).AddTicks(3543), "User", 2L, "One", null, null, "0a041b9462caa4a31bac3567e0b6e6fd9100787db2ab433d96f6d178cabfce90" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "user1");
        }
    }
}
