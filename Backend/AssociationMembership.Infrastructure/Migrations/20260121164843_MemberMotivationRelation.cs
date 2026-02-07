using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssociationMembership.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MemberMotivationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Memberships");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConsentAcceptedDate",
                table: "Memberships",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotivationText",
                table: "Memberships",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivationText",
                table: "Memberships");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConsentAcceptedDate",
                table: "Memberships",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Memberships",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
