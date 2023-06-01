using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Data.Migrations
{
    public partial class FillToDoItemStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ToDoItemStatuses (Name) VALUES ('New'), ('Approved'), ('In Progress'), ('Completed')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
