using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class updatePersonExpense2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonExpense_Group_GroupId",
                table: "PersonExpense");

            migrationBuilder.DropIndex(
                name: "IX_PersonExpense_GroupId",
                table: "PersonExpense");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "PersonExpense");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "PersonExpense",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonExpense_GroupId",
                table: "PersonExpense",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonExpense_Group_GroupId",
                table: "PersonExpense",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "ID");
        }
    }
}
