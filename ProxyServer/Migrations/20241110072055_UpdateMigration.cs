using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProxyServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccessPropertyID",
                table: "AccessProperties",
                newName: "AccessPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessProperties_ServiceName",
                table: "AccessProperties",
                column: "ServiceName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccessProperties_ServiceName",
                table: "AccessProperties");

            migrationBuilder.RenameColumn(
                name: "AccessPropertyId",
                table: "AccessProperties",
                newName: "AccessPropertyID");
        }
    }
}
