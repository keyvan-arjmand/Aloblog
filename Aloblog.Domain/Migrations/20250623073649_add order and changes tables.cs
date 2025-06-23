using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aloblog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addorderandchangestables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorySections");

            migrationBuilder.DropTable(
                name: "DesignItems");

            migrationBuilder.DropTable(
                name: "DesignTrees");

            migrationBuilder.DropColumn(
                name: "Alt",
                table: "WorkFlows");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "WorkFlows");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "WorkFlows");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "WorkFlows");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Faqs");

            migrationBuilder.AddColumn<string>(
                name: "Alt",
                table: "WorkFlowItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "WorkFlowItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "WorkFlowItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "MediaUrl",
                table: "MediaGrids",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Magazines",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MediaUrl",
                table: "Faqs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FaqItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "BlogDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Footers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Footers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropColumn(
                name: "Alt",
                table: "WorkFlowItems");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "WorkFlowItems");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "WorkFlowItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FaqItems");

            migrationBuilder.AddColumn<string>(
                name: "Alt",
                table: "WorkFlows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "WorkFlows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "WorkFlows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "WorkFlows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "MediaUrl",
                table: "MediaGrids",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Magazines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "MediaUrl",
                table: "Faqs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Faqs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Faqs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "BlogDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "CategorySections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignTrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignTreeId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignItems_DesignTrees_DesignTreeId",
                        column: x => x.DesignTreeId,
                        principalTable: "DesignTrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesignItems_DesignTreeId",
                table: "DesignItems",
                column: "DesignTreeId");
        }
    }
}
