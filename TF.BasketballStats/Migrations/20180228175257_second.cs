using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TF.BasketballStats.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameTime",
                table: "GameEvents");

            migrationBuilder.AddColumn<long>(
                name: "GameTimeMS",
                table: "GameEvents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameTimeMS",
                table: "GameEvents");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "GameTime",
                table: "GameEvents",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
