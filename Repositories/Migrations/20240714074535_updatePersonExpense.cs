using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class updatePersonExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__PersonExp__Group__3493CFA7",
                table: "PersonExpense");

            migrationBuilder.DropPrimaryKey(
                name: "PK__PersonEx__35F3AA9E2F5ABE97",
                table: "PersonExpense");

            migrationBuilder.RenameColumn(
                name: "GroupID",
                table: "PersonExpense",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonExpense_GroupID",
                table: "PersonExpense",
                newName: "IX_PersonExpense_GroupId");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "PersonExpense",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ReportId",
                table: "PersonExpense",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__PersonEx__35F3AA9E2F5ABE97",
                table: "PersonExpense",
                columns: new[] { "ExpenseID", "PersonID" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonExpense_ReportId",
                table: "PersonExpense",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonExpense_Group_GroupId",
                table: "PersonExpense",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonExpense_Report_ReportId",
                table: "PersonExpense",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonExpense_Group_GroupId",
                table: "PersonExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonExpense_Report_ReportId",
                table: "PersonExpense");

            migrationBuilder.DropPrimaryKey(
                name: "PK__PersonEx__35F3AA9E2F5ABE97",
                table: "PersonExpense");

            migrationBuilder.DropIndex(
                name: "IX_PersonExpense_ReportId",
                table: "PersonExpense");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "PersonExpense");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "PersonExpense",
                newName: "GroupID");

            migrationBuilder.RenameIndex(
                name: "IX_PersonExpense_GroupId",
                table: "PersonExpense",
                newName: "IX_PersonExpense_GroupID");

            migrationBuilder.AlterColumn<string>(
                name: "GroupID",
                table: "PersonExpense",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__PersonEx__35F3AA9E2F5ABE97",
                table: "PersonExpense",
                columns: new[] { "ExpenseID", "PersonID", "GroupID" });

            migrationBuilder.AddForeignKey(
                name: "FK__PersonExp__Group__3493CFA7",
                table: "PersonExpense",
                column: "GroupID",
                principalTable: "Group",
                principalColumn: "ID");
        }
    }
}
