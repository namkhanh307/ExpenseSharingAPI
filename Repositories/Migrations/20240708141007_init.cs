using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    size = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonGroup",
                columns: table => new
                {
                    personID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    groupID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PersonRo__290798144CC3970E", x => new { x.personID, x.groupID });
                    table.ForeignKey(
                        name: "FK__PersonRoo__Perso__5FB337D6",
                        column: x => x.personID,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__PersonRoo__RoomI__60A75C0F",
                        column: x => x.groupID,
                        principalTable: "Group",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    groupID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    personID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Report__3214EC2761BA3C9B", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Group",
                        column: x => x.groupID,
                        principalTable: "Group",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Report_Person",
                        column: x => x.personID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    expenseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    expenseType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    amount = table.Column<double>(type: "float", nullable: true),
                    personID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    reportID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    invoiceImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_Person",
                        column: x => x.personID,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expense_Report",
                        column: x => x.reportID,
                        principalTable: "Report",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonExpense",
                columns: table => new
                {
                    ExpenseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersonID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PersonEx__35F3AA9E2F5ABE97", x => new { x.ExpenseID, x.PersonID, x.GroupID });
                    table.ForeignKey(
                        name: "FK__PersonExp__Expen__32AB8735",
                        column: x => x.ExpenseID,
                        principalTable: "Expense",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__PersonExp__Group__3493CFA7",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__PersonExp__Perso__339FAB6E",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    personID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    expenseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    reportID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: true),
                    isPaid = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Record__D825197E756EB7D9", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Record_Expense",
                        column: x => x.expenseID,
                        principalTable: "Expense",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Record_Person",
                        column: x => x.personID,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Record_Report",
                        column: x => x.reportID,
                        principalTable: "Report",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_personID",
                table: "Expense",
                column: "personID");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_reportID",
                table: "Expense",
                column: "reportID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonExpense_GroupID",
                table: "PersonExpense",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonExpense_PersonID",
                table: "PersonExpense",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGroup_groupID",
                table: "PersonGroup",
                column: "groupID");

            migrationBuilder.CreateIndex(
                name: "IX_Record_expenseID",
                table: "Record",
                column: "expenseID");

            migrationBuilder.CreateIndex(
                name: "IX_Record_personID",
                table: "Record",
                column: "personID");

            migrationBuilder.CreateIndex(
                name: "IX_Record_reportID",
                table: "Record",
                column: "reportID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_groupID",
                table: "Report",
                column: "groupID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_personID",
                table: "Report",
                column: "personID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonExpense");

            migrationBuilder.DropTable(
                name: "PersonGroup");

            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
