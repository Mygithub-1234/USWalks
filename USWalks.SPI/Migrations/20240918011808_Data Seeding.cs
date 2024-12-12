using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace USWalks.SPI.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("157c896b-ca66-4348-9549-d3befda23367"), "Medium" },
                    { new Guid("280de39a-f6e1-4788-a72f-a5b4771ab3b1"), "High" },
                    { new Guid("2b4cc99c-cd73-43bc-b8fc-0ce67ab4504a"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("b2f933b5-2e35-4264-bbeb-86b24f20ebee"), "ST", "Seatle", "" },
                    { new Guid("c28493e0-a5b5-498b-ad13-64a4a020c601"), "DC", "Washington DC", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("157c896b-ca66-4348-9549-d3befda23367"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("280de39a-f6e1-4788-a72f-a5b4771ab3b1"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("2b4cc99c-cd73-43bc-b8fc-0ce67ab4504a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b2f933b5-2e35-4264-bbeb-86b24f20ebee"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c28493e0-a5b5-498b-ad13-64a4a020c601"));
        }
    }
}
