using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssociationMembership.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantGroupToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. TenantGroupId kolonunu nullable olarak ekle
            migrationBuilder.AddColumn<int>(
                name: "TenantGroupId",
                table: "Users",
                type: "integer",
                nullable: true);

            // 2. Mevcut Users'a default TenantGroupId ata
            migrationBuilder.Sql(@"
        UPDATE ""Users"" SET ""TenantGroupId"" = 2;
    ");

            // 3. Kolonu NOT NULL yap
            migrationBuilder.AlterColumn<int>(
                name: "TenantGroupId",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            // 4. Foreign key ekle
            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantGroupId",
                table: "Users",
                column: "TenantGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TenantGroups_TenantGroupId",
                table: "Users",
                column: "TenantGroupId",
                principalTable: "TenantGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TenantGroups_TenantGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TenantGroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TenantGroupId",
                table: "Users");
        }
    }
}
