using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoRu.Migrations
{
    public partial class _202041702 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "DormitoryNo",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "Guardian",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "GuardianPhone",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "HouseholdType",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "IsBoarding",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "MajorsId",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "Nation",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "QQ",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ImportStudentDtos");

            migrationBuilder.DropColumn(
                name: "StudentNub",
                table: "ImportStudentDtos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ImportStudentDtos",
                type: "varchar(200) CHARACTER SET utf8mb4",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CampusId",
                table: "ImportStudentDtos",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClassId",
                table: "ImportStudentDtos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DormitoryNo",
                table: "ImportStudentDtos",
                type: "varchar(20) CHARACTER SET utf8mb4",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "ImportStudentDtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "GradeId",
                table: "ImportStudentDtos",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guardian",
                table: "ImportStudentDtos",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardianPhone",
                table: "ImportStudentDtos",
                type: "varchar(20) CHARACTER SET utf8mb4",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseholdType",
                table: "ImportStudentDtos",
                type: "varchar(10) CHARACTER SET utf8mb4",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBoarding",
                table: "ImportStudentDtos",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MajorsId",
                table: "ImportStudentDtos",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nation",
                table: "ImportStudentDtos",
                type: "varchar(2) CHARACTER SET utf8mb4",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "ImportStudentDtos",
                type: "varchar(20) CHARACTER SET utf8mb4",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QQ",
                table: "ImportStudentDtos",
                type: "varchar(30) CHARACTER SET utf8mb4",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "ImportStudentDtos",
                type: "varchar(200) CHARACTER SET utf8mb4",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "ImportStudentDtos",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ImportStudentDtos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentNub",
                table: "ImportStudentDtos",
                type: "varchar(30) CHARACTER SET utf8mb4",
                maxLength: 30,
                nullable: true);
        }
    }
}
