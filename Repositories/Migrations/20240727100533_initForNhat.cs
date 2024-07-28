using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class initForNhat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Persons_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Persons_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Persons_PersonId1",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Persons_PersonId2",
                table: "Friends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.RenameColumn(
                name: "PersonId2",
                table: "Friends",
                newName: "FriendId");

            migrationBuilder.RenameColumn(
                name: "PersonId1",
                table: "Friends",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_PersonId2",
                table: "Friends",
                newName: "IX_Friends_FriendId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "FriendRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "FriendRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Persons_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Persons_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Persons_FriendId",
                table: "Friends",
                column: "FriendId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Persons_PersonId",
                table: "Friends",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Persons_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Persons_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Persons_FriendId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Persons_PersonId",
                table: "Friends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "FriendRequests");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "Friends",
                newName: "PersonId2");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Friends",
                newName: "PersonId1");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_FriendId",
                table: "Friends",
                newName: "IX_Friends_PersonId2");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "FriendRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                columns: new[] { "SenderId", "ReceiverId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Persons_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Persons_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Persons_PersonId1",
                table: "Friends",
                column: "PersonId1",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Persons_PersonId2",
                table: "Friends",
                column: "PersonId2",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
