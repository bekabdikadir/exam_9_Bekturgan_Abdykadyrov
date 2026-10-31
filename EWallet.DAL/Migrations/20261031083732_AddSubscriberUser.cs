using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EWallet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriberUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Subscribers",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 7,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 8,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 9,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 10,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 11,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 12,
                column: "UserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_UserId",
                table: "Subscribers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribers_AspNetUsers_UserId",
                table: "Subscribers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscribers_AspNetUsers_UserId",
                table: "Subscribers");

            migrationBuilder.DropIndex(
                name: "IX_Subscribers_UserId",
                table: "Subscribers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subscribers");
        }
    }
}
