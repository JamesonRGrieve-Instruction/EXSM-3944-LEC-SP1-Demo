using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVCAuth.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "person",
                type: "char(12)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "person",
                keyColumn: "id",
                keyValue: -2,
                column: "phone_number",
                value: "");

            migrationBuilder.UpdateData(
                table: "person",
                keyColumn: "id",
                keyValue: -1,
                column: "phone_number",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "person");
        }
    }
}
