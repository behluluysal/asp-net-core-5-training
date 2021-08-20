using Microsoft.EntityFrameworkCore.Migrations;

namespace rokWebsite.Data.Migrations
{
    public partial class addPlayersToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasePlayers",
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
                    RssSupportCurrent = table.Column<double>(type: "float", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Dkp = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePlayers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasePlayers");
        }
    }
}
