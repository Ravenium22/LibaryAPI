using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kutuphane.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLibraryAddLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitaplar_Libraries_LibraryId",
                table: "Kitaplar");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Kitaplar_LibraryId",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Kitaplar");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IadeTarihi",
                table: "Oduncler",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Konum",
                table: "Kitaplar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RafNo",
                table: "Kitaplar",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Konum",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "RafNo",
                table: "Kitaplar");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IadeTarihi",
                table: "Oduncler",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LibraryId",
                table: "Kitaplar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KitapSayisi = table.Column<int>(type: "int", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UyeSayisi = table.Column<int>(type: "int", nullable: false),
                    İsim = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_LibraryId",
                table: "Kitaplar",
                column: "LibraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kitaplar_Libraries_LibraryId",
                table: "Kitaplar",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
