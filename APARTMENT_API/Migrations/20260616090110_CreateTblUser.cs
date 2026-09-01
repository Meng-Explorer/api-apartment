using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APARTMENT_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateTblUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Username = table.Column<string>(type: "varchar2(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar2(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar2(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "number(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUser", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblUser");
        }
    }
}
