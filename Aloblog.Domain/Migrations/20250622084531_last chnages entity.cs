using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aloblog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class lastchnagesentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignItems_DesignTrees_DesignTreeId",
                table: "DesignItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FaqItems_Faqs_FaqId",
                table: "FaqItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowItems_WorkFlows_WorkFlowId",
                table: "WorkFlowItems");

            migrationBuilder.AlterColumn<int>(
                name: "WorkFlowId",
                table: "WorkFlowItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FaqId",
                table: "FaqItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DesignTreeId",
                table: "DesignItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DesignItems_DesignTrees_DesignTreeId",
                table: "DesignItems",
                column: "DesignTreeId",
                principalTable: "DesignTrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FaqItems_Faqs_FaqId",
                table: "FaqItems",
                column: "FaqId",
                principalTable: "Faqs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowItems_WorkFlows_WorkFlowId",
                table: "WorkFlowItems",
                column: "WorkFlowId",
                principalTable: "WorkFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignItems_DesignTrees_DesignTreeId",
                table: "DesignItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FaqItems_Faqs_FaqId",
                table: "FaqItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowItems_WorkFlows_WorkFlowId",
                table: "WorkFlowItems");

            migrationBuilder.AlterColumn<int>(
                name: "WorkFlowId",
                table: "WorkFlowItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FaqId",
                table: "FaqItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DesignTreeId",
                table: "DesignItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DesignItems_DesignTrees_DesignTreeId",
                table: "DesignItems",
                column: "DesignTreeId",
                principalTable: "DesignTrees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FaqItems_Faqs_FaqId",
                table: "FaqItems",
                column: "FaqId",
                principalTable: "Faqs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowItems_WorkFlows_WorkFlowId",
                table: "WorkFlowItems",
                column: "WorkFlowId",
                principalTable: "WorkFlows",
                principalColumn: "Id");
        }
    }
}
