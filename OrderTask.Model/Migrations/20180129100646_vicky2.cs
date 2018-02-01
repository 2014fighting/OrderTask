using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OrderTask.Model.Migrations
{
    public partial class vicky2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_sys_UserInfo_t_Order_OrderId",
                table: "t_sys_UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_t_sys_UserInfo_OrderId",
                table: "t_sys_UserInfo");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "t_sys_UserInfo");

            migrationBuilder.CreateTable(
                name: "ReceivePerson",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreteTime = table.Column<DateTime>(nullable: false),
                    CreteUser = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: true),
                    ReceiveState = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UserInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivePerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceivePerson_t_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "t_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivePerson_t_sys_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "t_sys_UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePerson_OrderId",
                table: "ReceivePerson",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePerson_UserInfoId",
                table: "ReceivePerson",
                column: "UserInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceivePerson");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "t_sys_UserInfo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_sys_UserInfo_OrderId",
                table: "t_sys_UserInfo",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_sys_UserInfo_t_Order_OrderId",
                table: "t_sys_UserInfo",
                column: "OrderId",
                principalTable: "t_Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
