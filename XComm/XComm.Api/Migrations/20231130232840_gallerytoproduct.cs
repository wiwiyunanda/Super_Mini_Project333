using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XComm.Api.Migrations
{
    /// <inheritdoc />
    public partial class gallerytoproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GalleryId",
                table: "MasterProducts",
                type: "bigint",
                nullable: true);

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
                column: "CreatedDate",
                value: new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2505));

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin",
                column: "CreatedDate",
                value: new DateTime(2023, 12, 1, 6, 28, 40, 611, DateTimeKind.Local).AddTicks(2361));

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

            migrationBuilder.CreateIndex(
                name: "IX_MasterProducts_GalleryId",
                table: "MasterProducts",
                column: "GalleryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterProducts_Galleries_GalleryId",
                table: "MasterProducts",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterProducts_Galleries_GalleryId",
                table: "MasterProducts");

            migrationBuilder.DropIndex(
                name: "IX_MasterProducts_GalleryId",
                table: "MasterProducts");

            migrationBuilder.DropColumn(
                name: "GalleryId",
                table: "MasterProducts");

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8997));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8999));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8999));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(9000));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(9001));

            migrationBuilder.UpdateData(
                table: "AuthorizationGroups",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(9001));

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "admin",
                column: "CreatedDate",
                value: new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "MasterAccount",
                keyColumn: "UserName",
                keyValue: "user1",
                column: "CreatedDate",
                value: new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8851));

            migrationBuilder.UpdateData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8974));

            migrationBuilder.UpdateData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 29, 9, 29, 7, 817, DateTimeKind.Local).AddTicks(8975));
        }
    }
}
