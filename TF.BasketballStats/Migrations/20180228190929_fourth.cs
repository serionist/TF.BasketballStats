using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TF.BasketballStats.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameTimeMS",
                table: "GameEvents");

            migrationBuilder.AddColumn<long>(
                name: "QuarterTimeMS",
                table: "GameEvents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuarterTimeMS",
                table: "GameEvents");

            migrationBuilder.AddColumn<long>(
                name: "GameTimeMS",
                table: "GameEvents",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
