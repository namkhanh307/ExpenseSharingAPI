using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class initEntityv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroups_Groups_GroupId",
                table: "PersonGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroups_Persons_PersonId",
                table: "PersonGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Expenses_ExpenseId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_ExpenseId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonGroups",
                table: "PersonGroups");

            migrationBuilder.DropIndex(
                name: "IX_PersonGroups_GroupId",
                table: "PersonGroups");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "PersonGroups",
                newName: "GroupID");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "PersonGroups",
                newName: "PersonID");

            migrationBuilder.AddColumn<string>(
                name: "GroupID",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonID",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ExpenseId",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ExpenseID",
                table: "Records",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonID",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReportID",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "GroupID",
                table: "PersonGroups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PersonID",
                table: "PersonGroups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "PersonGroups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "PersonGroups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonID",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReportID",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonID",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReportID",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonGroups",
                table: "PersonGroups",
                columns: new[] { "PersonId", "GroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_Records_ExpenseID",
                table: "Records",
                column: "ExpenseID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGroups_GroupId",
                table: "PersonGroups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGroups_Groups_GroupId",
                table: "PersonGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGroups_Persons_PersonId",
                table: "PersonGroups",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Expenses_ExpenseID",
                table: "Records",
                column: "ExpenseID",
                principalTable: "Expenses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroups_Groups_GroupId",
                table: "PersonGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroups_Persons_PersonId",
                table: "PersonGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Expenses_ExpenseID",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_ExpenseID",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonGroups",
                table: "PersonGroups");

            migrationBuilder.DropIndex(
                name: "IX_PersonGroups_GroupId",
                table: "PersonGroups");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ExpenseID",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "ReportID",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PersonGroups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "PersonGroups");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ReportID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ReportID",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "PersonID",
                table: "PersonGroups",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "GroupID",
                table: "PersonGroups",
                newName: "GroupId");

            migrationBuilder.AlterColumn<string>(
                name: "ExpenseId",
                table: "Records",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "PersonGroups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "PersonGroups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonGroups",
                table: "PersonGroups",
                columns: new[] { "PersonId", "GroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_Records_ExpenseId",
                table: "Records",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGroups_GroupId",
                table: "PersonGroups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGroups_Groups_GroupId",
                table: "PersonGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGroups_Persons_PersonId",
                table: "PersonGroups",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Expenses_ExpenseId",
                table: "Records",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
