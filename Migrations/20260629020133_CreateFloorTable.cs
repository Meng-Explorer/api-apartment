using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APARTMENT_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateFloorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblFloor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FloorNo = table.Column<byte>(type: "number(3)", nullable: false),
                    BuildingId = table.Column<decimal>(type: "number", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblFloor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblFloor_TblBuilding_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "TblBuilding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblFloor_BuildingId",
                table: "TblFloor",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblFloor");
        }
    }
}
