using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Abroad.Migrations
{
    public partial class Program_In_Course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseLevel",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "ProgramLevelsProgram_Id",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Program_Id",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProgramLevelsProgram_Id",
                table: "Courses",
                column: "ProgramLevelsProgram_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_ProgramLevels_ProgramLevelsProgram_Id",
                table: "Courses",
                column: "ProgramLevelsProgram_Id",
                principalTable: "ProgramLevels",
                principalColumn: "Program_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ProgramLevels_ProgramLevelsProgram_Id",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ProgramLevelsProgram_Id",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ProgramLevelsProgram_Id",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Program_Id",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "CourseLevel",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
