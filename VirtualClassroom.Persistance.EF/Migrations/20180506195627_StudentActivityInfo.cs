using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VirtualClassroom.Persistence.EF.Migrations
{
    public partial class StudentActivityInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityInfo_Activities_ActivityId",
                table: "ActivityInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityInfo_ActivityOccurences_OccurenceDateId",
                table: "ActivityInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityInfo_Students_StudentId",
                table: "ActivityInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentActivity_Activities_ActivityId",
                table: "StudentActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentActivity_Students_StudentId",
                table: "StudentActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentActivity",
                table: "StudentActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityInfo",
                table: "ActivityInfo");

            migrationBuilder.RenameTable(
                name: "StudentActivity",
                newName: "StudentActivities");

            migrationBuilder.RenameTable(
                name: "ActivityInfo",
                newName: "ActivityInfos");

            migrationBuilder.RenameIndex(
                name: "IX_StudentActivity_StudentId",
                table: "StudentActivities",
                newName: "IX_StudentActivities_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentActivity_ActivityId",
                table: "StudentActivities",
                newName: "IX_StudentActivities_ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityInfo_StudentId",
                table: "ActivityInfos",
                newName: "IX_ActivityInfos_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityInfo_OccurenceDateId",
                table: "ActivityInfos",
                newName: "IX_ActivityInfos_OccurenceDateId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityInfo_ActivityId",
                table: "ActivityInfos",
                newName: "IX_ActivityInfos_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentActivities",
                table: "StudentActivities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityInfos",
                table: "ActivityInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityInfos_Activities_ActivityId",
                table: "ActivityInfos",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityInfos_ActivityOccurences_OccurenceDateId",
                table: "ActivityInfos",
                column: "OccurenceDateId",
                principalTable: "ActivityOccurences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityInfos_Students_StudentId",
                table: "ActivityInfos",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentActivities_Activities_ActivityId",
                table: "StudentActivities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentActivities_Students_StudentId",
                table: "StudentActivities",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityInfos_Activities_ActivityId",
                table: "ActivityInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityInfos_ActivityOccurences_OccurenceDateId",
                table: "ActivityInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityInfos_Students_StudentId",
                table: "ActivityInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentActivities_Activities_ActivityId",
                table: "StudentActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentActivities_Students_StudentId",
                table: "StudentActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentActivities",
                table: "StudentActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityInfos",
                table: "ActivityInfos");

            migrationBuilder.RenameTable(
                name: "StudentActivities",
                newName: "StudentActivity");

            migrationBuilder.RenameTable(
                name: "ActivityInfos",
                newName: "ActivityInfo");

            migrationBuilder.RenameIndex(
                name: "IX_StudentActivities_StudentId",
                table: "StudentActivity",
                newName: "IX_StudentActivity_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentActivities_ActivityId",
                table: "StudentActivity",
                newName: "IX_StudentActivity_ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityInfos_StudentId",
                table: "ActivityInfo",
                newName: "IX_ActivityInfo_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityInfos_OccurenceDateId",
                table: "ActivityInfo",
                newName: "IX_ActivityInfo_OccurenceDateId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityInfos_ActivityId",
                table: "ActivityInfo",
                newName: "IX_ActivityInfo_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentActivity",
                table: "StudentActivity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityInfo",
                table: "ActivityInfo",
                column: "Id");

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
                name: "FK_ActivityInfo_Students_StudentId",
                table: "ActivityInfo",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentActivity_Activities_ActivityId",
                table: "StudentActivity",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentActivity_Students_StudentId",
                table: "StudentActivity",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
