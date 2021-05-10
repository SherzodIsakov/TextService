using Microsoft.EntityFrameworkCore.Migrations;

namespace TextService.Repositories.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TextEntities",
                table: "TextEntities");

            migrationBuilder.RenameTable(
                name: "TextEntities",
                newName: "TextEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextEntity",
                table: "TextEntity",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TextEntity",
                table: "TextEntity");

            migrationBuilder.RenameTable(
                name: "TextEntity",
                newName: "TextEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextEntities",
                table: "TextEntities",
                column: "Id");
        }
    }
}
