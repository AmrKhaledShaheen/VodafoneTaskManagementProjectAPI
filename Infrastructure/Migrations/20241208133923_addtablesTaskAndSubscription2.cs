using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtablesTaskAndSubscription2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VodafoneUserId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VodafoneUserId",
                table: "Subscriptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_VodafoneUserId",
                table: "Tasks",
                column: "VodafoneUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_VodafoneUserId",
                table: "Subscriptions",
                column: "VodafoneUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_AspNetUsers_VodafoneUserId",
                table: "Subscriptions",
                column: "VodafoneUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_VodafoneUserId",
                table: "Tasks",
                column: "VodafoneUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_AspNetUsers_VodafoneUserId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_VodafoneUserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_VodafoneUserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_VodafoneUserId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "VodafoneUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "VodafoneUserId",
                table: "Subscriptions");
        }
    }
}
