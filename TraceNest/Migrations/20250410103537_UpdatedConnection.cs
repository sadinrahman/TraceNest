using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraceNest.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Losts_CategoryId",
                table: "Losts");

            migrationBuilder.DropIndex(
                name: "IX_Losts_MunicipalityId",
                table: "Losts");

            migrationBuilder.CreateIndex(
                name: "IX_Losts_CategoryId",
                table: "Losts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Losts_MunicipalityId",
                table: "Losts",
                column: "MunicipalityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Losts_CategoryId",
                table: "Losts");

            migrationBuilder.DropIndex(
                name: "IX_Losts_MunicipalityId",
                table: "Losts");

            migrationBuilder.CreateIndex(
                name: "IX_Losts_CategoryId",
                table: "Losts",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Losts_MunicipalityId",
                table: "Losts",
                column: "MunicipalityId",
                unique: true);
        }
    }
}
