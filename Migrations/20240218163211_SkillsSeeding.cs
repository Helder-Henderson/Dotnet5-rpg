using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNET_RPG.Migrations
{
    public partial class SkillsSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { new Guid("26f3b710-b11d-42f5-aa88-1ab4a9ef6cca"), 15, "Frenzy" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { new Guid("99c41d77-67e3-4827-bbcd-a3aaacb7ebc8"), 20, "Zap" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { new Guid("b7327880-a7fa-49f2-9ad4-ad9306495fc4"), 25, "Fireball" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("26f3b710-b11d-42f5-aa88-1ab4a9ef6cca"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("99c41d77-67e3-4827-bbcd-a3aaacb7ebc8"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("b7327880-a7fa-49f2-9ad4-ad9306495fc4"));
        }
    }
}
