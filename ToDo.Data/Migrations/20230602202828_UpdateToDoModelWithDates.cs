using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Data.Migrations
{
    public partial class UpdateToDoModelWithDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StatusCompleted",
                table: "ToDoItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusInProgress",
                table: "ToDoItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusNew",
                table: "ToDoItems",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCompleted",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "StatusInProgress",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "StatusNew",
                table: "ToDoItems");
        }
    }
}
