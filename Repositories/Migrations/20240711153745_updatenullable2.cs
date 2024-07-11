using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class updatenullable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Person",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Expense_personID",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "personID",
                table: "Expense");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "personID",
                table: "Expense",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expense_personID",
                table: "Expense",
                column: "personID");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Person",
                table: "Expense",
                column: "personID",
                principalTable: "Person",
                principalColumn: "Id");
        }
    }
}
