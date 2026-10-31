using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EWallet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AutoRequisites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdentifierDigits",
                table: "Vendors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IdentifierPrefix",
                table: "Vendors",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Vendors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdentifierDigits", "IdentifierPrefix" },
                values: new object[] { 6, "0555" });

            migrationBuilder.UpdateData(
                table: "Vendors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IdentifierDigits", "IdentifierPrefix" },
                values: new object[] { 6, "0700" });

            migrationBuilder.UpdateData(
                table: "Vendors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdentifierDigits", "IdentifierPrefix" },
                values: new object[] { 6, "0770" });

            migrationBuilder.UpdateData(
                table: "Vendors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IdentifierDigits", "IdentifierPrefix" },
                values: new object[] { 5, "SAIMA-" });

            migrationBuilder.UpdateData(
                table: "Vendors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IdentifierDigits", "IdentifierPrefix" },
                values: new object[] { 5, "ELCAT-" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentifierDigits",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "IdentifierPrefix",
                table: "Vendors");
        }
    }
}
