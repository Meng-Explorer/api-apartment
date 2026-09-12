using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APARTMENT_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateTblBuilding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblBuilding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NameEnglish = table.Column<string>(type: "varchar2(50)", maxLength: 50, nullable: false),
                    NameKhmer = table.Column<string>(type: "nvarchar2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBuilding", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblBuilding");
        }
    }
}
