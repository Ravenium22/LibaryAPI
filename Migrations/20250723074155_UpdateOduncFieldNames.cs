using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kutuphane.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOduncFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeslimTarihi",
                table: "Oduncler",
                newName: "OduncAlinmaTarihi");

            migrationBuilder.RenameColumn(
                name: "OduncTarihi",
                table: "Oduncler",
                newName: "GeriVerilmesiGerekenTarih");

            migrationBuilder.RenameColumn(
                name: "IadeTarihi",
                table: "Oduncler",
                newName: "GeriVerilisTarihi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OduncAlinmaTarihi",
                table: "Oduncler",
                newName: "TeslimTarihi");

            migrationBuilder.RenameColumn(
                name: "GeriVerilmesiGerekenTarih",
                table: "Oduncler",
                newName: "OduncTarihi");

            migrationBuilder.RenameColumn(
                name: "GeriVerilisTarihi",
                table: "Oduncler",
                newName: "IadeTarihi");
        }
    }
}
