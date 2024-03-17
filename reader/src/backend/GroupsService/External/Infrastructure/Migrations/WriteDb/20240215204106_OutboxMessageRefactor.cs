using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.WriteDb
{
    /// <inheritdoc />
    public partial class OutboxMessageRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGroup_Entity_AllowedBooksId",
                table: "BookGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGroup_Entity_AllowedInGroupsId",
                table: "BookGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entity_AdminId",
                table: "Entity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entity_BookId",
                table: "Entity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entity_GroupId",
                table: "Entity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entity_UserBookProgressId",
                table: "Entity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entity_UserId",
                table: "Entity");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Entity_GroupsId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Entity_MembersId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_OutboxMessages_Entity_EntityId",
                table: "OutboxMessages");

            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_EntityId",
                table: "OutboxMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entity",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Entity_AdminId",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Entity_BookId",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Entity_GroupId",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Entity_UserBookProgressId",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Entity_UserId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "AuthorFirstName",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "AuthorLastName",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "BookName",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "LastReadSymbol",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "NotePosition",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "UserBookProgressId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Entity");

            migrationBuilder.RenameTable(
                name: "Entity",
                newName: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "OutboxMessages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessedAt",
                table: "OutboxMessages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookName = table.Column<string>(type: "text", nullable: false),
                    AuthorFirstName = table.Column<string>(type: "text", nullable: false),
                    AuthorLastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Users_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBookProgresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Progress = table.Column<int>(type: "integer", nullable: false),
                    LastReadSymbol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBookProgresses_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBookProgresses_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBookProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NotePosition = table.Column<int>(type: "integer", nullable: false),
                    UserBookProgressId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_UserBookProgresses_UserBookProgressId",
                        column: x => x.UserBookProgressId,
                        principalTable: "UserBookProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_AdminId",
                table: "Groups",
                column: "AdminId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserBookProgressId",
                table: "Notes",
                column: "UserBookProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookProgresses_BookId",
                table: "UserBookProgresses",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookProgresses_GroupId",
                table: "UserBookProgresses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookProgresses_UserId",
                table: "UserBookProgresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGroup_Books_AllowedBooksId",
                table: "BookGroup",
                column: "AllowedBooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGroup_Groups_AllowedInGroupsId",
                table: "BookGroup",
                column: "AllowedInGroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Groups_GroupsId",
                table: "GroupUser",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Users_MembersId",
                table: "GroupUser",
                column: "MembersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGroup_Books_AllowedBooksId",
                table: "BookGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGroup_Groups_AllowedInGroupsId",
                table: "BookGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Groups_GroupsId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Users_MembersId",
                table: "GroupUser");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "UserBookProgresses");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "ProcessedAt",
                table: "OutboxMessages");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Entity");

            migrationBuilder.AddColumn<Guid>(
                name: "EntityId",
                table: "OutboxMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "OutboxMessages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "OutboxMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Entity",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Entity",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "Entity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorFirstName",
                table: "Entity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorLastName",
                table: "Entity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "Entity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookName",
                table: "Entity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Entity",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Entity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Entity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastReadSymbol",
                table: "Entity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotePosition",
                table: "Entity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "Entity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Entity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserBookProgressId",
                table: "Entity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Entity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entity",
                table: "Entity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_EntityId",
                table: "OutboxMessages",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_AdminId",
                table: "Entity",
                column: "AdminId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entity_BookId",
                table: "Entity",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_GroupId",
                table: "Entity",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_UserBookProgressId",
                table: "Entity",
                column: "UserBookProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_UserId",
                table: "Entity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGroup_Entity_AllowedBooksId",
                table: "BookGroup",
                column: "AllowedBooksId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGroup_Entity_AllowedInGroupsId",
                table: "BookGroup",
                column: "AllowedInGroupsId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entity_AdminId",
                table: "Entity",
                column: "AdminId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entity_BookId",
                table: "Entity",
                column: "BookId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entity_GroupId",
                table: "Entity",
                column: "GroupId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entity_UserBookProgressId",
                table: "Entity",
                column: "UserBookProgressId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entity_UserId",
                table: "Entity",
                column: "UserId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Entity_GroupsId",
                table: "GroupUser",
                column: "GroupsId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Entity_MembersId",
                table: "GroupUser",
                column: "MembersId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutboxMessages_Entity_EntityId",
                table: "OutboxMessages",
                column: "EntityId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
