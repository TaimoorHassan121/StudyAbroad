using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Abroad.Migrations
{
    public partial class AppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationDetails",
                columns: table => new
                {
                    AppDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    StudentProfileStudentId = table.Column<int>(nullable: true),
                    UniversityId = table.Column<int>(nullable: false),
                    CourseId = table.Column<long>(nullable: true),
                    CourseIntakeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDetails", x => x.AppDetailId);
                    table.ForeignKey(
                        name: "FK_ApplicationDetails_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationDetails_CourseIntakes_CourseIntakeId",
                        column: x => x.CourseIntakeId,
                        principalTable: "CourseIntakes",
                        principalColumn: "CourseIntakeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationDetails_StudentProfiles_StudentProfileStudentId",
                        column: x => x.StudentProfileStudentId,
                        principalTable: "StudentProfiles",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationDetails_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDetails_CourseId",
                table: "ApplicationDetails",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDetails_CourseIntakeId",
                table: "ApplicationDetails",
                column: "CourseIntakeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDetails_StudentProfileStudentId",
                table: "ApplicationDetails",
                column: "StudentProfileStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDetails_UniversityId",
                table: "ApplicationDetails",
                column: "UniversityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationDetails");
        }
    }
}
