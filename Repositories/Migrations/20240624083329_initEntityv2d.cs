using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class initEntityv2d : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Persons_PersonId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Reports_ReportId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Persons_PersonId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Reports_ReportId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroups_Groups_GroupId",
                table: "PersonGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroups_Persons_PersonId",
                table: "PersonGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Persons_PersonId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Reports_ReportId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Groups_GroupId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Persons_PersonId",
                table: "Reports");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Persons_PersonId",
                table: "Expenses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Reports_ReportId",
                table: "Expenses",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Persons_PersonId",
                table: "Items",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Reports_ReportId",
                table: "Items",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGroups_Groups_GroupId",
                table: "PersonGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGroups_Persons_PersonId",
                table: "PersonGroups",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Persons_PersonId",
                table: "Records",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Reports_ReportId",
                table: "Records",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Groups_GroupId",
                table: "Reports",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Persons_PersonId",
                table: "Reports",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Persons_PersonId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Reports_ReportId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Persons_PersonId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Reports_ReportId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroups_Groups_GroupId",
                table: "PersonGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroups_Persons_PersonId",
                table: "PersonGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Persons_PersonId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Reports_ReportId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Groups_GroupId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Persons_PersonId",
                table: "Reports");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Persons_PersonId",
                table: "Expenses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Reports_ReportId",
                table: "Expenses",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Persons_PersonId",
                table: "Items",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Reports_ReportId",
                table: "Items",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Records_Persons_PersonId",
                table: "Records",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Reports_ReportId",
                table: "Records",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Groups_GroupId",
                table: "Reports",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Persons_PersonId",
                table: "Reports",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
