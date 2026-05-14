using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RebuildVoucherDocumentTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoucherDocumentTypeId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VoucherDocumentTypes",
                columns: table => new
                {
                    VoucherDocumentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    AuditCreateUser = table.Column<int>(type: "int", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditUpdateUser = table.Column<int>(type: "int", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "int", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherDocumentTypes", x => x.VoucherDocumentTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_VoucherDocumentTypeId",
                table: "Sales",
                column: "VoucherDocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_VoucherDocumentTypes_VoucherDocumentTypeId",
                table: "Sales",
                column: "VoucherDocumentTypeId",
                principalTable: "VoucherDocumentTypes",
                principalColumn: "VoucherDocumentTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_VoucherDocumentTypes_VoucherDocumentTypeId",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "VoucherDocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Sales_VoucherDocumentTypeId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "VoucherDocumentTypeId",
                table: "Sales");
        }
    }
}
