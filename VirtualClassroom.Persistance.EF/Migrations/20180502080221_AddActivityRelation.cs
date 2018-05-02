using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VirtualClassroom.Persistence.EF.Migrations
{
    public partial class AddActivityRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityInfo_ActivityOccurence_OccurenceDateId",
                table: "ActivityInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityOccurence_Activities_ActivityId",
                table: "ActivityOccurence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityOccurence",
                table: "ActivityOccurence");

            migrationBuilder.RenameTable(
                name: "ActivityOccurence",
                newName: "ActivityOccurences");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityOccurence_ActivityId",
                table: "ActivityOccurences",
                newName: "IX_ActivityOccurences_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityOccurences",
                table: "ActivityOccurences",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInfo_ActivityId",
                table: "ActivityInfo",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityInfo_Activities_ActivityId",
                table: "ActivityInfo",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityInfo_ActivityOccurences_OccurenceDateId",
                table: "ActivityInfo",
                column: "OccurenceDateId",
                principalTable: "ActivityOccurences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityOccurences_Activities_ActivityId",
                table: "ActivityOccurences",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityInfo_Activities_ActivityId",
                table: "ActivityInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityInfo_ActivityOccurences_OccurenceDateId",
                table: "ActivityInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityOccurences_Activities_ActivityId",
                table: "ActivityOccurences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityOccurences",
                table: "ActivityOccurences");

            migrationBuilder.DropIndex(
                name: "IX_ActivityInfo_ActivityId",
                table: "ActivityInfo");

            migrationBuilder.RenameTable(
                name: "ActivityOccurences",
                newName: "ActivityOccurence");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityOccurences_ActivityId",
                table: "ActivityOccurence",
                newName: "IX_ActivityOccurence_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityOccurence",
                table: "ActivityOccurence",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityInfo_ActivityOccurence_OccurenceDateId",
                table: "ActivityInfo",
                column: "OccurenceDateId",
                principalTable: "ActivityOccurence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityOccurence_Activities_ActivityId",
                table: "ActivityOccurence",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
