using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P7CreateRestApi.Migrations.UsersDb
{
    /// <inheritdoc />
    public partial class majusersdbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0840c1f0-e68b-4676-97b5-e80ab93aee2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b9ed021-5ab3-40d6-96d7-ee4013ad58f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "835c56f2-e8fd-4cf2-aa10-14f084c76736");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b36f3ec9-1256-4f1a-b6b6-b421677e3258");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0840c1f0-e68b-4676-97b5-e80ab93aee2b", "3", "Updator", "Updator" },
                    { "4b9ed021-5ab3-40d6-96d7-ee4013ad58f4", "4", "SimpleUser", "SimpleUser" },
                    { "835c56f2-e8fd-4cf2-aa10-14f084c76736", "2", "Creator", "Creator" },
                    { "b36f3ec9-1256-4f1a-b6b6-b421677e3258", "1", "Admin", "Admin" }
                });
        }
    }
}
