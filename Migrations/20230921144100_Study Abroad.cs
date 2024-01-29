using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Abroad.Migrations
{
    public partial class StudyAbroad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReqId",
                table: "ApplicationDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequairementReqId",
                table: "ApplicationDetails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Requairement",
                columns: table => new
                {
                    ReqId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Passportpic = table.Column<string>(nullable: true),
                    Passport_Status = table.Column<bool>(nullable: false),
                    Personal_Statement = table.Column<string>(nullable: true),
                    Personal_Status = table.Column<bool>(nullable: false),
                    Privacy_Statement = table.Column<string>(nullable: true),
                    Privacy_Status = table.Column<bool>(nullable: false),
                    Addational_Document = table.Column<string>(nullable: true),
                    Additional_Status = table.Column<bool>(nullable: false),
                    Bank_Statement = table.Column<string>(nullable: true),
                    Bank_Status = table.Column<bool>(nullable: false),
                    Medical_Statement = table.Column<string>(nullable: true),
                    Medical_Status = table.Column<bool>(nullable: false),
                    Emergency_Contact = table.Column<string>(nullable: true),
                    Emergency_Email = table.Column<string>(nullable: true),
                    Refree_Name = table.Column<string>(nullable: true),
                    Refree_Conteat = table.Column<string>(nullable: true),
                    Refree_Email = table.Column<string>(nullable: true),
                    Refree_Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requairement", x => x.ReqId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDetails_RequairementReqId",
                table: "ApplicationDetails",
                column: "RequairementReqId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDetails_Requairement_RequairementReqId",
                table: "ApplicationDetails",
                column: "RequairementReqId",
                principalTable: "Requairement",
                principalColumn: "ReqId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDetails_Requairement_RequairementReqId",
                table: "ApplicationDetails");

            migrationBuilder.DropTable(
                name: "Requairement");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationDetails_RequairementReqId",
                table: "ApplicationDetails");

            migrationBuilder.DropColumn(
                name: "ReqId",
                table: "ApplicationDetails");

            migrationBuilder.DropColumn(
                name: "RequairementReqId",
                table: "ApplicationDetails");
        }
    }
}
