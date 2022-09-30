using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartVotingSystem.UI.Data.Migrations
{
    public partial class initmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartiesMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartiesMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoteMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoterId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyVoted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VotedPartyType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteMaster", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartiesMaster");

            migrationBuilder.DropTable(
                name: "VoteMaster");
        }
    }
}
