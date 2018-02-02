using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OrderTask.Model.Migrations
{
    public partial class wenqing6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_t_sys_UserInfo_TrueName",
                table: "t_sys_UserInfo",
                column: "TrueName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_sys_UserInfo_UserName",
                table: "t_sys_UserInfo",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_t_sys_UserInfo_TrueName",
                table: "t_sys_UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_t_sys_UserInfo_UserName",
                table: "t_sys_UserInfo");
        }
    }
}
