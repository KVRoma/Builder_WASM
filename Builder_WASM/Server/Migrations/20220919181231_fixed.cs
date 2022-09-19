using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Builder_WASM.Server.Migrations
{
    public partial class @fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DSapplierId",
                table: "DItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DSapplierId",
                table: "DItems",
                type: "int",
                nullable: true);
        }
    }
}
