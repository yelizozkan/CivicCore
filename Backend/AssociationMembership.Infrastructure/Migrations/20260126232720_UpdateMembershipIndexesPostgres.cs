using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssociationMembership.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMembershipIndexesPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Memberships_TenantGroupId_IdentityNumber",
                table: "Memberships");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TenantGroupId_IdentityNumber",
                table: "Memberships",
                columns: new[] { "TenantGroupId", "IdentityNumber" },
                unique: true,
                filter: "\"IsDeleted\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Memberships_TenantGroupId_IdentityNumber",
                table: "Memberships");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TenantGroupId_IdentityNumber",
                table: "Memberships",
                columns: new[] { "TenantGroupId", "IdentityNumber" },
                unique: true,
                filter: "[IsDeleted] = 0");
        }
    }
}
