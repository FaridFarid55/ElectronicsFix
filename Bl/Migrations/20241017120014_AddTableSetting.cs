using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bl.Migrations
{
    /// <inheritdoc />
    public partial class AddTableSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Copyright = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Facebook_Link = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Googol_Link = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Twitter_Link = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Instagram_Link = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    LinkedIn_Link = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSettings", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbSettings");
        }
    }
}
