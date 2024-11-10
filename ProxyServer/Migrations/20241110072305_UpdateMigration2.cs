using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProxyServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccessProperties_ServiceName",
                table: "AccessProperties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccessProperties_ServiceName",
                table: "AccessProperties",
                column: "ServiceName",
                unique: true);
        }
    }
}
