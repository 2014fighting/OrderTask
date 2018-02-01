using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OrderTask.Model.Migrations
{
    public partial class vicky6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_Order_t_OrderType_OrderTypeId",
                table: "t_Order");

            migrationBuilder.DropIndex(
                name: "IX_t_Order_OrderTypeId",
                table: "t_Order");

            migrationBuilder.DropColumn(
                name: "OrderTypeId",
                table: "t_Order");

            migrationBuilder.AddColumn<string>(
                name: "OrderTypeIds",
                table: "t_Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTypeIds",
                table: "t_Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderTypeId",
                table: "t_Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_Order_OrderTypeId",
                table: "t_Order",
                column: "OrderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_Order_t_OrderType_OrderTypeId",
                table: "t_Order",
                column: "OrderTypeId",
                principalTable: "t_OrderType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
