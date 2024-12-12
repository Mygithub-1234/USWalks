using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USWalks.SPI.Migrations
{
    /// <inheritdoc />
    public partial class Updatetouploadtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FielName",
                table: "Images",
                newName: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Images",
                newName: "FielName");
        }
    }
}
