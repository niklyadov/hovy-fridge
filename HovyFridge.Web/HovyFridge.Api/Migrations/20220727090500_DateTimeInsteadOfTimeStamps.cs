using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HovyFridgeApi.Migrations
{
    public partial class DateTimeInsteadOfTimeStamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteTimestamp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeleteTimestamp",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "DeleteTimestamp",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "DeleteTimestamp",
                table: "ProductSuggestion");

            migrationBuilder.DropColumn(
                name: "CreatedTimestamp",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeleteTimestamp",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastEditedTimestamp",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeleteTimestamp",
                table: "Fridges");

            migrationBuilder.DropColumn(
                name: "DeleteTimestamp",
                table: "FridgeAccessLevels");

            migrationBuilder.AlterColumn<int>(
                name: "UserRole",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "ShoppingLists",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "Recipes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "ProductSuggestion",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditedDateTime",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "Fridges",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "FridgeAccessLevels",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "ProductSuggestion");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastEditedDateTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "Fridges");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "FridgeAccessLevels");

            migrationBuilder.AlterColumn<long>(
                name: "UserRole",
                table: "Users",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "DeleteTimestamp",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleteTimestamp",
                table: "ShoppingLists",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleteTimestamp",
                table: "Recipes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleteTimestamp",
                table: "ProductSuggestion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedTimestamp",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleteTimestamp",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastEditedTimestamp",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleteTimestamp",
                table: "Fridges",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleteTimestamp",
                table: "FridgeAccessLevels",
                type: "bigint",
                nullable: true);
        }
    }
}
