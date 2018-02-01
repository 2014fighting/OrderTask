using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OrderTask.Model.Migrations
{
    public partial class wenqing4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompleteTime",
                table: "t_ReceivePerson",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiveTime",
                table: "t_ReceivePerson",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteTime",
                table: "t_ReceivePerson");

            migrationBuilder.DropColumn(
                name: "ReceiveTime",
                table: "t_ReceivePerson");
        }
    }
}
