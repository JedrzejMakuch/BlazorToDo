using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Data.Migrations
{
    public partial class UpdateStatusModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoItemStatuses_ToDoItemStatusId",
                table: "ToDoItems");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_ToDoItemStatusId",
                table: "ToDoItems");

            migrationBuilder.AlterColumn<byte>(
                name: "ToDoItemStatusId",
                table: "ToDoItems",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ToDoItemStatusId",
                table: "ToDoItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoItemStatusId",
                table: "ToDoItems",
                column: "ToDoItemStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoItemStatuses_ToDoItemStatusId",
                table: "ToDoItems",
                column: "ToDoItemStatusId",
                principalTable: "ToDoItemStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
