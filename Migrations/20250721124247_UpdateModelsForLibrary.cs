using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kutuphane.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsForLibrary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitaplar_Kutuphaneler_KutuphaneId",
                table: "Kitaplar");

            migrationBuilder.DropForeignKey(
                name: "FK_Oduncler_Kullanicilar_KullanıcıId",
                table: "Oduncler");

            migrationBuilder.DropTable(
                name: "Kutuphaneler");

            migrationBuilder.DropIndex(
                name: "IX_Oduncler_KullanıcıId",
                table: "Oduncler");

            migrationBuilder.DropIndex(
                name: "IX_Kitaplar_KutuphaneId",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "KullanıcıId",
                table: "Oduncler");

            migrationBuilder.RenameColumn(
                name: "YayınTarihi",
                table: "Kitaplar",
                newName: "YayinTarihi");

            migrationBuilder.RenameColumn(
                name: "KutuphaneId",
                table: "Kitaplar",
                newName: "SayfaSayisi");

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
                    İsim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KitapSayisi = table.Column<int>(type: "int", nullable: false),
                    UyeSayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oduncler_KullaniciId",
                table: "Oduncler",
                column: "KullaniciId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Oduncler_Kullanicilar_KullaniciId",
                table: "Oduncler",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitaplar_Libraries_LibraryId",
                table: "Kitaplar");

            migrationBuilder.DropForeignKey(
                name: "FK_Oduncler_Kullanicilar_KullaniciId",
                table: "Oduncler");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Oduncler_KullaniciId",
                table: "Oduncler");

            migrationBuilder.DropIndex(
                name: "IX_Kitaplar_LibraryId",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Kitaplar");

            migrationBuilder.RenameColumn(
                name: "YayinTarihi",
                table: "Kitaplar",
                newName: "YayınTarihi");

            migrationBuilder.RenameColumn(
                name: "SayfaSayisi",
                table: "Kitaplar",
                newName: "KutuphaneId");

            migrationBuilder.AddColumn<int>(
                name: "KullanıcıId",
                table: "Oduncler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kutuphaneler",
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
                    table.PrimaryKey("PK_Kutuphaneler", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oduncler_KullanıcıId",
                table: "Oduncler",
                column: "KullanıcıId");

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_KutuphaneId",
                table: "Kitaplar",
                column: "KutuphaneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kitaplar_Kutuphaneler_KutuphaneId",
                table: "Kitaplar",
                column: "KutuphaneId",
                principalTable: "Kutuphaneler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Oduncler_Kullanicilar_KullanıcıId",
                table: "Oduncler",
                column: "KullanıcıId",
                principalTable: "Kullanicilar",
                principalColumn: "Id");
        }
    }
}
