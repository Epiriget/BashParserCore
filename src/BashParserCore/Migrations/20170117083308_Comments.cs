using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BashParserCore.Migrations
{
    public partial class Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostID",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "PostID",
                table: "Posts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PostID",
                table: "Comments",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Comments",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Posts",
                newName: "PostID");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Comments",
                newName: "PostID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                newName: "IX_Comments_PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostID",
                table: "Comments",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
