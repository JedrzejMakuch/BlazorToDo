using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Data.Migrations
{
    public partial class UpdateToDoItemModelWithCheckboxKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckboxItem_ToDoItems_ToDoItemId",
                table: "CheckboxItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckboxItem",
                table: "CheckboxItem");

            migrationBuilder.RenameTable(
                name: "CheckboxItem",
                newName: "Checkboxes");

            migrationBuilder.RenameIndex(
                name: "IX_CheckboxItem_ToDoItemId",
                table: "Checkboxes",
                newName: "IX_Checkboxes_ToDoItemId");

            migrationBuilder.AlterColumn<int>(
                name: "ToDoItemId",
                table: "Checkboxes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Checkboxes",
                table: "Checkboxes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkboxes_ToDoItems_ToDoItemId",
                table: "Checkboxes",
                column: "ToDoItemId",
                principalTable: "ToDoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkboxes_ToDoItems_ToDoItemId",
                table: "Checkboxes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Checkboxes",
                table: "Checkboxes");

            migrationBuilder.RenameTable(
                name: "Checkboxes",
                newName: "CheckboxItem");

            migrationBuilder.RenameIndex(
                name: "IX_Checkboxes_ToDoItemId",
                table: "CheckboxItem",
                newName: "IX_CheckboxItem_ToDoItemId");

            migrationBuilder.AlterColumn<int>(
                name: "ToDoItemId",
                table: "CheckboxItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckboxItem",
                table: "CheckboxItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckboxItem_ToDoItems_ToDoItemId",
                table: "CheckboxItem",
                column: "ToDoItemId",
                principalTable: "ToDoItems",
                principalColumn: "Id");
        }
    }
}
