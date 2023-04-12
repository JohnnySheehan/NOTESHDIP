using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NOTES_HDIP.Data.Migrations
{
    public partial class AddPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "NoteSpaces",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "NoteSpaces");
        }
    }
}
