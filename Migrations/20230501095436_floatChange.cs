using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Abroad.Migrations
{
    public partial class floatChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Grading_Score",
                table: "StudentProfiles",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Grade_Scale",
                table: "StudentProfiles",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Grading_Score",
                table: "StudentProfiles",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Grade_Scale",
                table: "StudentProfiles",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);
        }
    }
}
