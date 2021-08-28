using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rokWebsite.Data.Migrations
{
    public partial class AddedKvkTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Alliances_AllianceId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alliances",
                table: "Alliances");

            migrationBuilder.RenameTable(
                name: "Alliances",
                newName: "Alliance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alliance",
                table: "Alliance",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Kvk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opponents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KingdomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kvk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kvk_Kingdoms_KingdomId",
                        column: x => x.KingdomId,
                        principalTable: "Kingdoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Perfomance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    T4killsBase = table.Column<double>(type: "float", nullable: false),
                    T5killsBase = table.Column<double>(type: "float", nullable: false),
                    T4deathsBase = table.Column<double>(type: "float", nullable: false),
                    T5deathsBase = table.Column<double>(type: "float", nullable: false),
                    RssSupportBase = table.Column<double>(type: "float", nullable: false),
                    T4killsCurrent = table.Column<double>(type: "float", nullable: false),
                    T5killsCurrent = table.Column<double>(type: "float", nullable: false),
                    T4deathsCurrent = table.Column<double>(type: "float", nullable: false),
                    T5deathsCurrent = table.Column<double>(type: "float", nullable: false),
                    RssSupportCurrent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfomance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KvkPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KvkId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PerfomanceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkPlayers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KvkPlayers_Kvk_KvkId",
                        column: x => x.KvkId,
                        principalTable: "Kvk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KvkPlayers_Perfomance_PerfomanceId",
                        column: x => x.PerfomanceId,
                        principalTable: "Perfomance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kvk_KingdomId",
                table: "Kvk",
                column: "KingdomId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkPlayers_KvkId",
                table: "KvkPlayers",
                column: "KvkId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkPlayers_PerfomanceId",
                table: "KvkPlayers",
                column: "PerfomanceId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkPlayers_UserId",
                table: "KvkPlayers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Alliance_AllianceId",
                table: "AspNetUsers",
                column: "AllianceId",
                principalTable: "Alliance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Alliance_AllianceId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "KvkPlayers");

            migrationBuilder.DropTable(
                name: "Kvk");

            migrationBuilder.DropTable(
                name: "Perfomance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alliance",
                table: "Alliance");

            migrationBuilder.RenameTable(
                name: "Alliance",
                newName: "Alliances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alliances",
                table: "Alliances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Alliances_AllianceId",
                table: "AspNetUsers",
                column: "AllianceId",
                principalTable: "Alliances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
