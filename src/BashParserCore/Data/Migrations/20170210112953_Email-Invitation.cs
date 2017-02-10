using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BashParserCore.Data.Migrations
{
    public partial class EmailInvitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invitees",
                columns: table => new
                {
                    InvitationId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    SenderId = table.Column<string>(nullable: true),
                    SendingTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitees", x => x.InvitationId);
                    table.ForeignKey(
                        name: "FK_Invitees_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitees_SenderId",
                table: "Invitees",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitees");
        }
    }
}
