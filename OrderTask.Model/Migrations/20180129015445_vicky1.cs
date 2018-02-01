using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OrderTask.Model.Migrations
{
    public partial class vicky1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Evaluate",
                table: "t_Order");

            migrationBuilder.DropColumn(
                name: "JobScheduling",
                table: "t_Order");

            migrationBuilder.DropColumn(
                name: "ReceivePerson",
                table: "t_Order");

            migrationBuilder.AddColumn<DateTime>(
                name: "JobScheduling",
                table: "t_sys_UserInfo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "t_sys_UserInfo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "t_OrderLog",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_sys_UserInfo_OrderId",
                table: "t_sys_UserInfo",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_t_OrderLog_OrderId",
                table: "t_OrderLog",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_OrderLog_t_Order_OrderId",
                table: "t_OrderLog",
                column: "OrderId",
                principalTable: "t_Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_t_sys_UserInfo_t_Order_OrderId",
                table: "t_sys_UserInfo",
                column: "OrderId",
                principalTable: "t_Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_OrderLog_t_Order_OrderId",
                table: "t_OrderLog");

            migrationBuilder.DropForeignKey(
                name: "FK_t_sys_UserInfo_t_Order_OrderId",
                table: "t_sys_UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_t_sys_UserInfo_OrderId",
                table: "t_sys_UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_t_OrderLog_OrderId",
                table: "t_OrderLog");

            migrationBuilder.DropColumn(
                name: "JobScheduling",
                table: "t_sys_UserInfo");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "t_sys_UserInfo");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "t_OrderLog");

            migrationBuilder.AddColumn<string>(
                name: "Evaluate",
                table: "t_Order",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JobScheduling",
                table: "t_Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceivePerson",
                table: "t_Order",
                nullable: true);
        }
    }
}
