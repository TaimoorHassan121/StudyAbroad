using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Abroad.Migrations
{
    public partial class Study_Information : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Cities",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentNum = table.Column<string>(nullable: true),
                    F_Name = table.Column<string>(nullable: true),
                    M_Name = table.Column<string>(nullable: true),
                    L_Name = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Current_Country = table.Column<int>(nullable: true),
                    Postal_Code = table.Column<int>(nullable: true),
                    Gender = table.Column<bool>(nullable: false),
                    First_Language = table.Column<string>(nullable: true),
                    Marital_Status = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Passport_Num = table.Column<string>(nullable: true),
                    Passport_Expiry = table.Column<DateTime>(nullable: true),
                    Passport_Pic = table.Column<string>(nullable: true),
                    Country_of_Education = table.Column<int>(nullable: true),
                    Education_Level = table.Column<string>(nullable: true),
                    Grading_Scheme = table.Column<string>(nullable: true),
                    Grade_Scale = table.Column<double>(nullable: true),
                    Grading_Score = table.Column<double>(nullable: true),
                    Graduated_From = table.Column<bool>(nullable: false),
                    IsValid_Visa = table.Column<bool>(nullable: false),
                    IsStudy_Permit = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Referral_Source = table.Column<string>(nullable: true),
                    Country_Of_Intrest = table.Column<string>(nullable: true),
                    Service_Of_Intrest = table.Column<string>(nullable: true),
                    Confirmation = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducationDetails",
                columns: table => new
                {
                    Edu_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    studentProfileStudentId = table.Column<int>(nullable: true),
                    Country_Of_Institute = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    Institute_Name = table.Column<string>(nullable: true),
                    Education_Level = table.Column<string>(nullable: true),
                    Degree_Name = table.Column<string>(nullable: true),
                    Institute_Language = table.Column<string>(nullable: true),
                    Degree_Start = table.Column<DateTime>(nullable: true),
                    Degree_End = table.Column<DateTime>(nullable: true),
                    Graduate_From = table.Column<bool>(nullable: false),
                    Graduation_Date = table.Column<DateTime>(nullable: true),
                    Physical_Certificate = table.Column<bool>(nullable: false),
                    EDU_Province = table.Column<string>(nullable: true),
                    EDU_City = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Postal_Code = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDetails", x => x.Edu_Id);
                    table.ForeignKey(
                        name: "FK_EducationDetails_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationDetails_StudentProfiles_studentProfileStudentId",
                        column: x => x.studentProfileStudentId,
                        principalTable: "StudentProfiles",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GmatTests",
                columns: table => new
                {
                    GmatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    GMAT_Exame = table.Column<bool>(nullable: false),
                    GMAT_Exam_Date = table.Column<DateTime>(nullable: true),
                    GMAT_Verbal = table.Column<double>(nullable: false),
                    GMAT_Verbal_Rank = table.Column<double>(nullable: false),
                    GMAT_Quantitative = table.Column<double>(nullable: false),
                    GMAT_Quantitative_Rank = table.Column<double>(nullable: false),
                    GMAT_Writting = table.Column<double>(nullable: false),
                    GMAT_Writting_Rank = table.Column<double>(nullable: false),
                    GMAT_Total = table.Column<double>(nullable: false),
                    GMAT_Total_Rank = table.Column<double>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GmatTests", x => x.GmatId);
                    table.ForeignKey(
                        name: "FK_GmatTests_StudentProfiles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentProfiles",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GreTest",
                columns: table => new
                {
                    GreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    GRE_Exame = table.Column<bool>(nullable: false),
                    GRE_Exam_Date = table.Column<DateTime>(nullable: true),
                    GRE_Verbal = table.Column<double>(nullable: false),
                    GRE_Verbal_Rank = table.Column<double>(nullable: false),
                    GRE_Quantitative = table.Column<double>(nullable: false),
                    GRE_Quantitative_Rank = table.Column<double>(nullable: false),
                    GRE_Writting = table.Column<double>(nullable: false),
                    GRE_Writting_Rank = table.Column<double>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreTest", x => x.GreId);
                    table.ForeignKey(
                        name: "FK_GreTest_StudentProfiles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentProfiles",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageTests",
                columns: table => new
                {
                    LanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    Language_Type = table.Column<string>(nullable: true),
                    Exam_Date = table.Column<DateTime>(nullable: true),
                    Reading_Score = table.Column<double>(nullable: false),
                    Listening_Score = table.Column<double>(nullable: false),
                    Writing_Score = table.Column<double>(nullable: false),
                    Speaking_Score = table.Column<double>(nullable: false),
                    Overall_Score = table.Column<double>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTests", x => x.LanguageId);
                    table.ForeignKey(
                        name: "FK_LanguageTests_StudentProfiles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentProfiles",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationDetails_CountryId",
                table: "EducationDetails",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationDetails_studentProfileStudentId",
                table: "EducationDetails",
                column: "studentProfileStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GmatTests_StudentId",
                table: "GmatTests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GreTest_StudentId",
                table: "GreTest",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTests_StudentId",
                table: "LanguageTests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_CityId",
                table: "StudentProfiles",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_CountryId",
                table: "StudentProfiles",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_StateId",
                table: "StudentProfiles",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropTable(
                name: "EducationDetails");

            migrationBuilder.DropTable(
                name: "GmatTests");

            migrationBuilder.DropTable(
                name: "GreTest");

            migrationBuilder.DropTable(
                name: "LanguageTests");

            migrationBuilder.DropTable(
                name: "StudentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Cities");
        }
    }
}
