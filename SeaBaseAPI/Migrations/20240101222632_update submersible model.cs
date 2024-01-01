using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeaBaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatesubmersiblemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submersibles_Personnel_PilotId",
                table: "Submersibles");

            migrationBuilder.AlterColumn<int>(
                name: "PilotId",
                table: "Submersibles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Submersibles_Personnel_PilotId",
                table: "Submersibles",
                column: "PilotId",
                principalTable: "Personnel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submersibles_Personnel_PilotId",
                table: "Submersibles");

            migrationBuilder.AlterColumn<int>(
                name: "PilotId",
                table: "Submersibles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Submersibles_Personnel_PilotId",
                table: "Submersibles",
                column: "PilotId",
                principalTable: "Personnel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
