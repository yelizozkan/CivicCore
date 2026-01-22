using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssociationMembership.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMembershipIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Memberships_IdentityNumber",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_MobilePhone",
                table: "Memberships");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Memberships_TenantGroupId_IdentityNumber",
                table: "Memberships");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_IdentityNumber",
                table: "Memberships",
                column: "IdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_MobilePhone",
                table: "Memberships",
                column: "MobilePhone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TenantGroupId_IdentityNumber",
                table: "Memberships",
                columns: new[] { "TenantGroupId", "IdentityNumber" },
                unique: true);
        }
    }
}
