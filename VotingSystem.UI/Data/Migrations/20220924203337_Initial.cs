using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartVotingSystem.UI.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteMaster",
                table: "VoteMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartiesMaster",
                table: "PartiesMaster");

            migrationBuilder.RenameTable(
                name: "VoteMaster",
                newName: "VoteMasters");

            migrationBuilder.RenameTable(
                name: "PartiesMaster",
                newName: "PartiesMasters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteMasters",
                table: "VoteMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartiesMasters",
                table: "PartiesMasters",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteMasters",
                table: "VoteMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartiesMasters",
                table: "PartiesMasters");

            migrationBuilder.RenameTable(
                name: "VoteMasters",
                newName: "VoteMaster");

            migrationBuilder.RenameTable(
                name: "PartiesMasters",
                newName: "PartiesMaster");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteMaster",
                table: "VoteMaster",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartiesMaster",
                table: "PartiesMaster",
                column: "Id");
        }
    }
}
