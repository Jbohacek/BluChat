using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BluChat.Core.Migrations
{
    /// <inheritdoc />
    public partial class fixedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbUsers_tbChats_ChatId",
                table: "tbUsers");

            migrationBuilder.DropIndex(
                name: "IX_tbUsers_ChatId",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "tbUsers");

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    ChatsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => new { x.ChatsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ChatUser_tbChats_ChatsId",
                        column: x => x.ChatsId,
                        principalTable: "tbChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUser_tbUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_UsersId",
                table: "ChatUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatUser");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatId",
                table: "tbUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbUsers_ChatId",
                table: "tbUsers",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbUsers_tbChats_ChatId",
                table: "tbUsers",
                column: "ChatId",
                principalTable: "tbChats",
                principalColumn: "Id");
        }
    }
}
