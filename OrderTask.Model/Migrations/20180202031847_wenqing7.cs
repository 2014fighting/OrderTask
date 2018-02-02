using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OrderTask.Model.Migrations
{
    public partial class wenqing7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_t_OrderLog_Operator",
                table: "t_OrderLog",
                column: "Operator");

            migrationBuilder.AddForeignKey(
                name: "FK_t_OrderLog_t_sys_UserInfo_Operator",
                table: "t_OrderLog",
                column: "Operator",
                principalTable: "t_sys_UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_OrderLog_t_sys_UserInfo_Operator",
                table: "t_OrderLog");

            migrationBuilder.DropIndex(
                name: "IX_t_OrderLog_Operator",
                table: "t_OrderLog");
        }
    }
}
