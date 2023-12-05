using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XComm.Api.Migrations
{
    /// <inheritdoc />
    public partial class addCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_MasterProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "MasterProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(230));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(231));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(232));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(233));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(233));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(234));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(235));

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin",
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(98));

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "user1",
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(111));

            migrationBuilder.UpdateData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(215));

            migrationBuilder.UpdateData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 5, 11, 23, 1, 428, DateTimeKind.Local).AddTicks(216));

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

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
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7845));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7846));

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin",
                column: "CreatedDate",
                value: new DateTime(2023, 12, 4, 10, 26, 40, 180, DateTimeKind.Local).AddTicks(7592));

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
    }
}
