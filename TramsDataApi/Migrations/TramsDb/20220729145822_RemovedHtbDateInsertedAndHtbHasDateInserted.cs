using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TramsDataApi.Migrations.TramsDb
{
    public partial class RemovedHtbDateInsertedAndHtbHasDateInserted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasHtbDateInserted",
                schema: "sdd",
                table: "AcademyTransferProjects");

            migrationBuilder.DropColumn(
                name: "HtbDateInserted",
                schema: "sdd",
                table: "AcademyTransferProjects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasHtbDateInserted",
                schema: "sdd",
                table: "AcademyTransferProjects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HtbDateInserted",
                schema: "sdd",
                table: "AcademyTransferProjects",
                type: "date",
                nullable: true);
        }
    }
}
