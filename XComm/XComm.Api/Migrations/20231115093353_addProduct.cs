using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XComm.Api.Migrations
{
    /// <inheritdoc />
    public partial class addProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<long>(type: "bigint", nullable: false),
                    Initial = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterProducts_MasterVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "MasterVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterVariants_Initial",
                table: "MasterVariants",
                column: "Initial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterCategories_Initial",
                table: "MasterCategories",
                column: "Initial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterCategories_Name",
                table: "MasterCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterProducts_Initial",
                table: "MasterProducts",
                column: "Initial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterProducts_Name",
                table: "MasterProducts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterProducts_VariantId",
                table: "MasterProducts",
                column: "VariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterProducts");

            migrationBuilder.DropIndex(
                name: "IX_MasterVariants_Initial",
                table: "MasterVariants");

            migrationBuilder.DropIndex(
                name: "IX_MasterCategories_Initial",
                table: "MasterCategories");

            migrationBuilder.DropIndex(
                name: "IX_MasterCategories_Name",
                table: "MasterCategories");
        }
    }
}
