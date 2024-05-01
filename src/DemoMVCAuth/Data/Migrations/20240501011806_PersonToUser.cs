using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVCAuth.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "person",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "person",
                keyColumn: "id",
                keyValue: -2,
                column: "user_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "person",
                keyColumn: "id",
                keyValue: -1,
                column: "user_id",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_person_user_id",
                table: "person",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_person_AspNetUsers_user_id",
                table: "person",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_person_AspNetUsers_user_id",
                table: "person");

            migrationBuilder.DropIndex(
                name: "IX_person_user_id",
                table: "person");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "person");
        }
    }
}
