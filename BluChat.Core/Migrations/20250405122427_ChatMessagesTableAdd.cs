using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BluChat.Core.Migrations
{
    /// <inheritdoc />
    public partial class ChatMessagesTableAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChatId",
                table: "tbUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tbChats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationOfCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastTimeEdited = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParentChatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SenderId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbChatMessages_tbChats_ParentChatId",
                        column: x => x.ParentChatId,
                        principalTable: "tbChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbChatMessages_tbUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbUsers_ChatId",
                table: "tbUsers",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_tbChatMessages_ParentChatId",
                table: "tbChatMessages",
                column: "ParentChatId");

            migrationBuilder.CreateIndex(
                name: "IX_tbChatMessages_SenderId",
                table: "tbChatMessages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbUsers_tbChats_ChatId",
                table: "tbUsers",
                column: "ChatId",
                principalTable: "tbChats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbUsers_tbChats_ChatId",
                table: "tbUsers");

            migrationBuilder.DropTable(
                name: "tbChatMessages");

            migrationBuilder.DropTable(
                name: "tbChats");

            migrationBuilder.DropIndex(
                name: "IX_tbUsers_ChatId",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "tbUsers");
        }
    }
}
