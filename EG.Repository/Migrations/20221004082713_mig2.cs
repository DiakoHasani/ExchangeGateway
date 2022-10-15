using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EG.Repository.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblBitcoinTblVoutBitcoin");

            migrationBuilder.CreateIndex(
                name: "IX_TblVoutBitcoins_BitcoinId",
                table: "TblVoutBitcoins",
                column: "BitcoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblVoutBitcoins_TblBitcoins_BitcoinId",
                table: "TblVoutBitcoins",
                column: "BitcoinId",
                principalTable: "TblBitcoins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblVoutBitcoins_TblBitcoins_BitcoinId",
                table: "TblVoutBitcoins");

            migrationBuilder.DropIndex(
                name: "IX_TblVoutBitcoins_BitcoinId",
                table: "TblVoutBitcoins");

            migrationBuilder.CreateTable(
                name: "TblBitcoinTblVoutBitcoin",
                columns: table => new
                {
                    BitcoinId = table.Column<int>(type: "int", nullable: false),
                    TblBitcoinsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBitcoinTblVoutBitcoin", x => new { x.BitcoinId, x.TblBitcoinsId });
                    table.ForeignKey(
                        name: "FK_TblBitcoinTblVoutBitcoin_TblBitcoins_TblBitcoinsId",
                        column: x => x.TblBitcoinsId,
                        principalTable: "TblBitcoins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblBitcoinTblVoutBitcoin_TblVoutBitcoins_BitcoinId",
                        column: x => x.BitcoinId,
                        principalTable: "TblVoutBitcoins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblBitcoinTblVoutBitcoin_TblBitcoinsId",
                table: "TblBitcoinTblVoutBitcoin",
                column: "TblBitcoinsId");
        }
    }
}
