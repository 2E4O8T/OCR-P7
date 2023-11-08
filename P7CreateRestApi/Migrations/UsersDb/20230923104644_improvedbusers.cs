using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P7CreateRestApi.Migrations.UsersDb
{
    /// <inheritdoc />
    public partial class improvedbusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1fc9b10e-e126-4f5e-ae32-d61d872e75bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c983ac4-4b0f-4e0c-91ba-eff89bd40b59");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bdca621-2043-4631-b6f6-61edf5329f02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5e717be-889d-4757-b355-6dc6392c8eec");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1fc9b10e-e126-4f5e-ae32-d61d872e75bb", "1", "Admin", "Administrator" },
                    { "4c983ac4-4b0f-4e0c-91ba-eff89bd40b59", "2", "Create", "Creator" },
                    { "9bdca621-2043-4631-b6f6-61edf5329f02", "4", "User", "SimpleUser" },
                    { "b5e717be-889d-4757-b355-6dc6392c8eec", "3", "Update", "Updator" }
                });
        }
    }
}
