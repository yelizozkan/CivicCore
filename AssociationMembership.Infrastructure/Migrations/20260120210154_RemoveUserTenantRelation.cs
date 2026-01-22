using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssociationMembership.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserTenantRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Tenants_TenantId",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Users_ApprovedBy",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_MembershipTrackings_Users_ApprovedBy",
                table: "MembershipTrackings");

            migrationBuilder.DropIndex(
                name: "IX_MembershipTrackings_ApprovedBy",
                table: "MembershipTrackings");

            migrationBuilder.DropIndex(
                name: "IX_MembershipTrackings_MembershipId",
                table: "MembershipTrackings");

            migrationBuilder.DropIndex(
                name: "IX_MembershipTrackings_MembershipId_Status",
                table: "MembershipTrackings");

            migrationBuilder.DropIndex(
                name: "IX_MembershipTrackings_PaymentDueDate",
                table: "MembershipTrackings");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_ApprovedBy",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_MobilePhone",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_TenantGroupId",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_TenantId_NationalId",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "MembershipTrackings");

            migrationBuilder.DropColumn(
                name: "FeeType",
                table: "MembershipTrackings");

            migrationBuilder.DropColumn(
                name: "PaymentDueDate",
                table: "MembershipTrackings");

            migrationBuilder.DropColumn(
                name: "PaymentReceived",
                table: "MembershipTrackings");

            migrationBuilder.DropColumn(
                name: "PaymentRequired",
                table: "MembershipTrackings");

            migrationBuilder.DropColumn(
                name: "ReceiptFilePath",
                table: "MembershipTrackings");

            migrationBuilder.DropColumn(
                name: "ReminderSent",
                table: "MembershipTrackings");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Memberships");

            migrationBuilder.RenameColumn(
                name: "PaymentNote",
                table: "MembershipTrackings",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "NationalId",
                table: "Memberships",
                newName: "IdentityNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Memberships_NationalId",
                table: "Memberships",
                newName: "IX_Memberships_IdentityNumber");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MembershipTrackings",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "WorkPhone",
                table: "Memberships",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "TenantGroupId",
                table: "Memberships",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Memberships",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipTrackings_MembershipId",
                table: "MembershipTrackings",
                column: "MembershipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembershipTrackings_Status",
                table: "MembershipTrackings",
                column: "Status");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MembershipTrackings_MembershipId",
                table: "MembershipTrackings");

            migrationBuilder.DropIndex(
                name: "IX_MembershipTrackings_Status",
                table: "MembershipTrackings");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_MobilePhone",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_TenantGroupId_IdentityNumber",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Memberships");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "MembershipTrackings",
                newName: "PaymentNote");

            migrationBuilder.RenameColumn(
                name: "IdentityNumber",
                table: "Memberships",
                newName: "NationalId");

            migrationBuilder.RenameIndex(
                name: "IX_Memberships_IdentityNumber",
                table: "Memberships",
                newName: "IX_Memberships_NationalId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MembershipTrackings",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "MembershipTrackings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeeType",
                table: "MembershipTrackings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDueDate",
                table: "MembershipTrackings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PaymentReceived",
                table: "MembershipTrackings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PaymentRequired",
                table: "MembershipTrackings",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptFilePath",
                table: "MembershipTrackings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReminderSent",
                table: "MembershipTrackings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "WorkPhone",
                table: "Memberships",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenantGroupId",
                table: "Memberships",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "Memberships",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Memberships",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Memberships",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MembershipTrackings_ApprovedBy",
                table: "MembershipTrackings",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipTrackings_MembershipId",
                table: "MembershipTrackings",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipTrackings_MembershipId_Status",
                table: "MembershipTrackings",
                columns: new[] { "MembershipId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipTrackings_PaymentDueDate",
                table: "MembershipTrackings",
                column: "PaymentDueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_ApprovedBy",
                table: "Memberships",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_MobilePhone",
                table: "Memberships",
                column: "MobilePhone",
                unique: true,
                filter: "\"MobilePhone\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TenantGroupId",
                table: "Memberships",
                column: "TenantGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TenantId_NationalId",
                table: "Memberships",
                columns: new[] { "TenantId", "NationalId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Tenants_TenantId",
                table: "Memberships",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Users_ApprovedBy",
                table: "Memberships",
                column: "ApprovedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipTrackings_Users_ApprovedBy",
                table: "MembershipTrackings",
                column: "ApprovedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
