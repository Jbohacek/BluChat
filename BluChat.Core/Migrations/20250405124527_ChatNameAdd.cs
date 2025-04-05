using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BluChat.Core.Migrations
{
    /// <inheritdoc />
    public partial class ChatNameAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "tbChats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnformatedMessage",
                table: "tbChatMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "tbChats");

            migrationBuilder.DropColumn(
                name: "UnformatedMessage",
                table: "tbChatMessages");
        }
    }
}
