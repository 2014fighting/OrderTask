using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OrderTask.Model.Migrations
{
    public partial class vicky6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaidiAddress",
                table: "t_DataManage");

            migrationBuilder.DropColumn(
                name: "BiaozhunAddress",
                table: "t_DataManage");

            migrationBuilder.DropColumn(
                name: "ChangjingAddress",
                table: "t_DataManage");

            migrationBuilder.DropColumn(
                name: "GuanggaoAddress",
                table: "t_DataManage");

            migrationBuilder.DropColumn(
                name: "MoteAddress",
                table: "t_DataManage");

            migrationBuilder.DropColumn(
                name: "RukouAddress",
                table: "t_DataManage");

            migrationBuilder.DropColumn(
                name: "TaojiaoAddress",
                table: "t_DataManage");

            migrationBuilder.DropColumn(
                name: "XiangxiAddress",
                table: "t_DataManage");

            migrationBuilder.RenameColumn(
                name: "ZhuantiAddress",
                table: "t_DataManage",
                newName: "DataAddress");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "t_DataManage",
                maxLength: 300,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "t_DataManage",
                maxLength: 300,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "t_DataManage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_t_DataManage_OrderId",
                table: "t_DataManage",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_DataManage_t_Order_OrderId",
                table: "t_DataManage",
                column: "OrderId",
                principalTable: "t_Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_DataManage_t_Order_OrderId",
                table: "t_DataManage");

            migrationBuilder.DropIndex(
                name: "IX_t_DataManage_OrderId",
                table: "t_DataManage");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "t_DataManage");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "t_DataManage");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "t_DataManage");

            migrationBuilder.RenameColumn(
                name: "DataAddress",
                table: "t_DataManage",
                newName: "ZhuantiAddress");

            migrationBuilder.AddColumn<string>(
                name: "BaidiAddress",
                table: "t_DataManage",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BiaozhunAddress",
                table: "t_DataManage",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangjingAddress",
                table: "t_DataManage",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuanggaoAddress",
                table: "t_DataManage",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoteAddress",
                table: "t_DataManage",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RukouAddress",
                table: "t_DataManage",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaojiaoAddress",
                table: "t_DataManage",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XiangxiAddress",
                table: "t_DataManage",
                maxLength: 300,
                nullable: true);
        }
    }
}
