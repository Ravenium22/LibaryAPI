using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kutuphane.Migrations
{
    /// <inheritdoc />
    public partial class AddPerformanceIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Oduncler_KullaniciId",
                table: "Oduncler",
                newName: "IX_Odunc_KullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_Oduncler_KitapId",
                table: "Oduncler",
                newName: "IX_Odunc_KitapId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Kullanicilar",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Kitaplar",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Baslik",
                table: "Kitaplar",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KapakUrl",
                table: "Kitaplar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Odunc_IadeEdildiMi",
                table: "Oduncler",
                column: "IadeEdildiMi");

            migrationBuilder.CreateIndex(
                name: "IX_Odunc_IadeEdildiMi_GeriVerilmeTarihi",
                table: "Oduncler",
                columns: new[] { "IadeEdildiMi", "GeriVerilmesiGerekenTarih" });

            migrationBuilder.CreateIndex(
                name: "IX_Kullanici_Email",
                table: "Kullanicilar",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Kitap_Baslik",
                table: "Kitaplar",
                column: "Baslik");

            migrationBuilder.CreateIndex(
                name: "IX_Kitap_ISBN",
                table: "Kitaplar",
                column: "ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_Kitap_MusaitMi",
                table: "Kitaplar",
                column: "MusaitMi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Odunc_IadeEdildiMi",
                table: "Oduncler");

            migrationBuilder.DropIndex(
                name: "IX_Odunc_IadeEdildiMi_GeriVerilmeTarihi",
                table: "Oduncler");

            migrationBuilder.DropIndex(
                name: "IX_Kullanici_Email",
                table: "Kullanicilar");

            migrationBuilder.DropIndex(
                name: "IX_Kitap_Baslik",
                table: "Kitaplar");

            migrationBuilder.DropIndex(
                name: "IX_Kitap_ISBN",
                table: "Kitaplar");

            migrationBuilder.DropIndex(
                name: "IX_Kitap_MusaitMi",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "KapakUrl",
                table: "Kitaplar");

            migrationBuilder.RenameIndex(
                name: "IX_Odunc_KullaniciId",
                table: "Oduncler",
                newName: "IX_Oduncler_KullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_Odunc_KitapId",
                table: "Oduncler",
                newName: "IX_Oduncler_KitapId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Kitaplar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Baslik",
                table: "Kitaplar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
