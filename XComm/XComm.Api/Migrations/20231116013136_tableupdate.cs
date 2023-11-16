using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XComm.Api.Migrations
{
    /// <inheritdoc />
    public partial class tableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterFileCollecitons",
                table: "MasterFileCollecitons");

            migrationBuilder.RenameTable(
                name: "MasterFileCollecitons",
                newName: "MasterFileCollections");

            migrationBuilder.RenameIndex(
                name: "IX_MasterFileCollecitons_Title",
                table: "MasterFileCollections",
                newName: "IX_MasterFileCollections_Title");

            migrationBuilder.RenameIndex(
                name: "IX_MasterFileCollecitons_FileName",
                table: "MasterFileCollections",
                newName: "IX_MasterFileCollections_FileName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterFileCollections",
                table: "MasterFileCollections",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterFileCollections",
                table: "MasterFileCollections");

            migrationBuilder.RenameTable(
                name: "MasterFileCollections",
                newName: "MasterFileCollecitons");

            migrationBuilder.RenameIndex(
                name: "IX_MasterFileCollections_Title",
                table: "MasterFileCollecitons",
                newName: "IX_MasterFileCollecitons_Title");

            migrationBuilder.RenameIndex(
                name: "IX_MasterFileCollections_FileName",
                table: "MasterFileCollecitons",
                newName: "IX_MasterFileCollecitons_FileName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterFileCollecitons",
                table: "MasterFileCollecitons",
                column: "Id");
        }
    }
}
