using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLibrary.Migrations
{
    /// <inheritdoc />
    public partial class BookOptionalBookset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookSets_BookSetId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "BookSetId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookSets_BookSetId",
                table: "Books",
                column: "BookSetId",
                principalTable: "BookSets",
                principalColumn: "BookSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookSets_BookSetId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "BookSetId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookSets_BookSetId",
                table: "Books",
                column: "BookSetId",
                principalTable: "BookSets",
                principalColumn: "BookSetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
