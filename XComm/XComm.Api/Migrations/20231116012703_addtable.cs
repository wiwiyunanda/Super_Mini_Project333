using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XComm.Api.Migrations
{
    /// <inheritdoc />
    public partial class addtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterAccount",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterAccount", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "MasterFileCollecitons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterFileCollecitons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterUserRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterUserRoles_MasterAccount_UserName",
                        column: x => x.UserName,
                        principalTable: "MasterAccount",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransOrderHeader_Reference",
                table: "TransOrderHeader",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterAccount_UserName",
                table: "MasterAccount",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterFileCollecitons_FileName",
                table: "MasterFileCollecitons",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterFileCollecitons_Title",
                table: "MasterFileCollecitons",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterUserRoles_UserName",
                table: "MasterUserRoles",
                column: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterFileCollecitons");

            migrationBuilder.DropTable(
                name: "MasterUserRoles");

            migrationBuilder.DropTable(
                name: "MasterAccount");

            migrationBuilder.DropIndex(
                name: "IX_TransOrderHeader_Reference",
                table: "TransOrderHeader");
        }
    }
}
