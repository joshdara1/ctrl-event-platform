using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTRL.Migrations
{
    /// <inheritdoc />
    public partial class AddDJRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DJId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_DJId",
                table: "Events",
                column: "DJId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_DJs_DJId",
                table: "Events",
                column: "DJId",
                principalTable: "DJs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_DJs_DJId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_DJId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DJId",
                table: "Events");
        }
    }
}
