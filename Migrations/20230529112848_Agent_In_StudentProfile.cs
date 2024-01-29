using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Abroad.Migrations
{
    public partial class Agent_In_StudentProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "StudentProfiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_AgentId",
                table: "StudentProfiles",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_Agents_AgentId",
                table: "StudentProfiles",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "AgentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_Agents_AgentId",
                table: "StudentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_AgentId",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "StudentProfiles");
        }
    }
}
