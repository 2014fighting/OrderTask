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
            migrationBuilder.CreateTable(
                name: "t_Evaluate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Communication = table.Column<float>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 20, nullable: true),
                    CreateUserId = table.Column<int>(nullable: true),
                    EvaluateInfo = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: true),
                    ReceivePersonId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    Satisfaction = table.Column<float>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(maxLength: 20, nullable: true),
                    UpdateUserId = table.Column<int>(nullable: true),
                    WorkProgress = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Evaluate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_Evaluate_t_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "t_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_Evaluate_t_ReceivePerson_ReceivePersonId",
                        column: x => x.ReceivePersonId,
                        principalTable: "t_ReceivePerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_Evaluate_OrderId",
                table: "t_Evaluate",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_t_Evaluate_ReceivePersonId",
                table: "t_Evaluate",
                column: "ReceivePersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_Evaluate");
        }
    }
}
