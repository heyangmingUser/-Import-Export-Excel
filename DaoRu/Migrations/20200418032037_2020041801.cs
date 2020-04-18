using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoRu.Migrations
{
    public partial class _2020041801 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "ImportStudentDtos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ImportStudentDtos");
        }
    }
}
