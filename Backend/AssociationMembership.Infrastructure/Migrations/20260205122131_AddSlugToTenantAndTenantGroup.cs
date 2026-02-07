using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssociationMembership.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToTenantAndTenantGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Tenants",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);  // Önce nullable

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "TenantGroups",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);  // Önce nullable

            // 2. Mevcut kayıtlara slug ata (Id bazlı benzersiz değerler)
            migrationBuilder.Sql(@"
                UPDATE ""Tenants"" 
                SET ""Slug"" = LOWER(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(""Name"", ' ', '-'), 'ç', 'c'), 'ğ', 'g'), 'ı', 'i'), 'ö', 'o'), 'ş', 's')) || '-' || ""Id""
                WHERE ""Slug"" IS NULL;
            ");

            migrationBuilder.Sql(@"
                UPDATE ""TenantGroups"" 
                SET ""Slug"" = LOWER(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(""Name"", ' ', '-'), 'ç', 'c'), 'ğ', 'g'), 'ı', 'i'), 'ö', 'o'), 'ş', 's')) || '-' || ""Id""
                WHERE ""Slug"" IS NULL;
            ");

            // 3. Şimdi NOT NULL yap
            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Tenants",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "TenantGroups",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            // 4. Unique index oluştur
            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Slug",
                table: "Tenants",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantGroups_Slug",
                table: "TenantGroups",
                column: "Slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TenantGroups_Slug",
                table: "TenantGroups");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_Slug",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "TenantGroups");
        }
    }
}
