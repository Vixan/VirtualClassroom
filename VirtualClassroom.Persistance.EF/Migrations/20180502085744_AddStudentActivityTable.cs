using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VirtualClassroom.Persistence.EF.Migrations
{
    public partial class AddStudentActivityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Students_StudentId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_StudentId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Activities");

            migrationBuilder.CreateTable(
                name: "StudentActivity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityId = table.Column<int>(nullable: true),
                    StudentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentActivity_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentActivity_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentActivity_ActivityId",
                table: "StudentActivity",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentActivity_StudentId",
                table: "StudentActivity",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentActivity");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Activities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_StudentId",
                table: "Activities",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Students_StudentId",
                table: "Activities",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
