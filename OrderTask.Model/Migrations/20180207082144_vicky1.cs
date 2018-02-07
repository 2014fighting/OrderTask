using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OrderTask.Model.Migrations
{
    public partial class vicky1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShotMapAddress",
                table: "t_DataManage",
                newName: "ZhuantiAddress");

            migrationBuilder.RenameColumn(
                name: "RuingMapAddress",
                table: "t_DataManage",
                newName: "XiangxiAddress");

            migrationBuilder.RenameColumn(
                name: "PictureAddress5",
                table: "t_DataManage",
                newName: "TaojiaoAddress");

            migrationBuilder.RenameColumn(
                name: "PictureAddress4",
                table: "t_DataManage",
                newName: "RukouAddress");

            migrationBuilder.RenameColumn(
                name: "PictureAddress3",
                table: "t_DataManage",
                newName: "MoteAddress");

            migrationBuilder.RenameColumn(
                name: "PictureAddress2",
                table: "t_DataManage",
                newName: "GuanggaoAddress");

            migrationBuilder.RenameColumn(
                name: "PictureAddress1",
                table: "t_DataManage",
                newName: "ChangjingAddress");

            migrationBuilder.RenameColumn(
                name: "FootMapAddress",
                table: "t_DataManage",
                newName: "BiaozhunAddress");

            migrationBuilder.RenameColumn(
                name: "DesignMapAddress",
                table: "t_DataManage",
                newName: "BaidiAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZhuantiAddress",
                table: "t_DataManage",
                newName: "ShotMapAddress");

            migrationBuilder.RenameColumn(
                name: "XiangxiAddress",
                table: "t_DataManage",
                newName: "RuingMapAddress");

            migrationBuilder.RenameColumn(
                name: "TaojiaoAddress",
                table: "t_DataManage",
                newName: "PictureAddress5");

            migrationBuilder.RenameColumn(
                name: "RukouAddress",
                table: "t_DataManage",
                newName: "PictureAddress4");

            migrationBuilder.RenameColumn(
                name: "MoteAddress",
                table: "t_DataManage",
                newName: "PictureAddress3");

            migrationBuilder.RenameColumn(
                name: "GuanggaoAddress",
                table: "t_DataManage",
                newName: "PictureAddress2");

            migrationBuilder.RenameColumn(
                name: "ChangjingAddress",
                table: "t_DataManage",
                newName: "PictureAddress1");

            migrationBuilder.RenameColumn(
                name: "BiaozhunAddress",
                table: "t_DataManage",
                newName: "FootMapAddress");

            migrationBuilder.RenameColumn(
                name: "BaidiAddress",
                table: "t_DataManage",
                newName: "DesignMapAddress");
        }
    }
}
