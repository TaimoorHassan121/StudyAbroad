using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Abroad.Migrations
{
    public partial class newAssessment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    AssessmentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryOfInterest = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    LanguageCertification = table.Column<string>(nullable: true),
                    CourseOfInterest = table.Column<string>(nullable: true),
                    UniversityOfInterest = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.AssessmentId);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentEducationalBackgrounds",
                columns: table => new
                {
                    AssessmentEducationalBackgroundId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentId = table.Column<long>(nullable: false),
                    Degree = table.Column<string>(nullable: true),
                    GraduatingYear = table.Column<DateTime>(nullable: false),
                    CourseDuration = table.Column<string>(nullable: true),
                    CGPA = table.Column<string>(nullable: true),
                    Institutions = table.Column<string>(nullable: true),
                    MajorSubjects = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentEducationalBackgrounds", x => x.AssessmentEducationalBackgroundId);
                    table.ForeignKey(
                        name: "FK_AssessmentEducationalBackgrounds_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentEducationalBackgrounds_AssessmentId",
                table: "AssessmentEducationalBackgrounds",
                column: "AssessmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentEducationalBackgrounds");

            migrationBuilder.DropTable(
                name: "Assessments");
        }
    }
}
