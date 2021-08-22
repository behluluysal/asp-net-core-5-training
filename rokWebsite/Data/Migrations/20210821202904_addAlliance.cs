using Microsoft.EntityFrameworkCore.Migrations;

namespace rokWebsite.Data.Migrations
{
    public partial class addAlliance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllianceId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Alliances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alliances", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AllianceId",
                table: "AspNetUsers",
                column: "AllianceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Alliances_AllianceId",
                table: "AspNetUsers",
                column: "AllianceId",
                principalTable: "Alliances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Alliances_AllianceId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Alliances");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AllianceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AllianceId",
                table: "AspNetUsers");
        }
    }
}
