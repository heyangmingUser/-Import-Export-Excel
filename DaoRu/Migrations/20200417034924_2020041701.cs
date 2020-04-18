using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoRu.Migrations
{
    public partial class _2020041701 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportStudentDtos",
                columns: table => new
                {
                    SerialNumber = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentCode = table.Column<string>(maxLength: 30, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IdCard = table.Column<string>(maxLength: 18, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    Guardian = table.Column<string>(maxLength: 50, nullable: false),
                    GuardianPhone = table.Column<string>(maxLength: 20, nullable: true),
                    StudentNub = table.Column<string>(maxLength: 30, nullable: true),
                    DormitoryNo = table.Column<string>(maxLength: 20, nullable: true),
                    QQ = table.Column<string>(maxLength: 30, nullable: true),
                    Nation = table.Column<string>(maxLength: 2, nullable: true),
                    HouseholdType = table.Column<string>(maxLength: 10, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    Status = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    IsBoarding = table.Column<bool>(nullable: true),
                    ClassId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: true),
                    CampusId = table.Column<Guid>(nullable: true),
                    MajorsId = table.Column<Guid>(nullable: true),
                    GradeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportStudentDtos", x => x.SerialNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportStudentDtos");
        }
    }
}
