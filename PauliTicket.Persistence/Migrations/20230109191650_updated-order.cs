using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PauliTicket.Persistence.Migrations
{
    public partial class updatedorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EventId",
                table: "Orders",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Events_EventId",
                table: "Orders",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Events_EventId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EventId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Orders");
        }
    }
}
