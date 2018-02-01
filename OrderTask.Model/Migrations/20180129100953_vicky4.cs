using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OrderTask.Model.Migrations
{
    public partial class vicky4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePerson_t_Order_OrderId",
                table: "ReceivePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePerson_t_sys_UserInfo_UserInfoId",
                table: "ReceivePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceivePerson",
                table: "ReceivePerson");

            migrationBuilder.RenameTable(
                name: "ReceivePerson",
                newName: "t_ReceivePerson");

            migrationBuilder.RenameIndex(
                name: "IX_ReceivePerson_UserInfoId",
                table: "t_ReceivePerson",
                newName: "IX_t_ReceivePerson_UserInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceivePerson_OrderId",
                table: "t_ReceivePerson",
                newName: "IX_t_ReceivePerson_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_ReceivePerson",
                table: "t_ReceivePerson",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_ReceivePerson_t_Order_OrderId",
                table: "t_ReceivePerson",
                column: "OrderId",
                principalTable: "t_Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_t_ReceivePerson_t_sys_UserInfo_UserInfoId",
                table: "t_ReceivePerson",
                column: "UserInfoId",
                principalTable: "t_sys_UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_ReceivePerson_t_Order_OrderId",
                table: "t_ReceivePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_t_ReceivePerson_t_sys_UserInfo_UserInfoId",
                table: "t_ReceivePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_ReceivePerson",
                table: "t_ReceivePerson");

            migrationBuilder.RenameTable(
                name: "t_ReceivePerson",
                newName: "ReceivePerson");

            migrationBuilder.RenameIndex(
                name: "IX_t_ReceivePerson_UserInfoId",
                table: "ReceivePerson",
                newName: "IX_ReceivePerson_UserInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_t_ReceivePerson_OrderId",
                table: "ReceivePerson",
                newName: "IX_ReceivePerson_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceivePerson",
                table: "ReceivePerson",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePerson_t_Order_OrderId",
                table: "ReceivePerson",
                column: "OrderId",
                principalTable: "t_Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePerson_t_sys_UserInfo_UserInfoId",
                table: "ReceivePerson",
                column: "UserInfoId",
                principalTable: "t_sys_UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
