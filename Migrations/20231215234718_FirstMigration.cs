using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SeaBaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personnel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Department = table.Column<int>(type: "integer", nullable: false),
                    IsDeployed = table.Column<bool>(type: "boolean", nullable: false),
                    SubmersibleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submersibles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VesselName = table.Column<string>(type: "text", nullable: false),
                    IsDeployed = table.Column<bool>(type: "boolean", nullable: false),
                    PilotId = table.Column<int>(type: "integer", nullable: false),
                    Condition = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submersibles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submersibles_Personnel_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_SubmersibleId",
                table: "Personnel",
                column: "SubmersibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Submersibles_PilotId",
                table: "Submersibles",
                column: "PilotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_Submersibles_SubmersibleId",
                table: "Personnel",
                column: "SubmersibleId",
                principalTable: "Submersibles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Submersibles_SubmersibleId",
                table: "Personnel");

            migrationBuilder.DropTable(
                name: "Submersibles");

            migrationBuilder.DropTable(
                name: "Personnel");
        }
    }
}
