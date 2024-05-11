using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVCAuth.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndustryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "industry_id",
                table: "job",
                type: "int",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.CreateTable(
                name: "industry",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_industry", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "industry",
                columns: new[] { "id", "name" },
                values: new object[] { -1, "Transportation" });

            migrationBuilder.UpdateData(
                table: "job",
                keyColumn: "id",
                keyValue: -1,
                column: "industry_id",
                value: -1);

            migrationBuilder.CreateIndex(
                name: "IX_job_industry_id",
                table: "job",
                column: "industry_id");

            migrationBuilder.AddForeignKey(
                name: "FK_job_industry_industry_id",
                table: "job",
                column: "industry_id",
                principalTable: "industry",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_job_industry_industry_id",
                table: "job");

            migrationBuilder.DropTable(
                name: "industry");

            migrationBuilder.DropIndex(
                name: "IX_job_industry_id",
                table: "job");

            migrationBuilder.DropColumn(
                name: "industry_id",
                table: "job");
        }
    }
}
