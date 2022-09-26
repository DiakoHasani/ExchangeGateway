using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EG.Repository.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblErrors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnerException = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblErrors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblWallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CoinType = table.Column<int>(type: "int", nullable: false),
                    TokenType = table.Column<int>(type: "int", nullable: true),
                    Last_Txid = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    WebhookAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblWallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblBitcoins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VinTransactionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VinValue = table.Column<double>(type: "float", nullable: false),
                    VoutReciverValue = table.Column<double>(type: "float", nullable: false),
                    VoutNetworkValue = table.Column<double>(type: "float", nullable: false),
                    Fee = table.Column<double>(type: "float", nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBitcoins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblBitcoins_TblWallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "TblWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblSellRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CoinType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSellRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblSellRequests_TblWallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "TblWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblTrons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FromAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ToAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Block = table.Column<long>(type: "bigint", nullable: false),
                    ContractAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Quant = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    ContractRet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FinalResult = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Revert = table.Column<bool>(type: "bit", nullable: false),
                    TokenId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TokenAbbr = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TokenType = table.Column<int>(type: "int", nullable: true),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTrons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblTrons_TblWallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "TblWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblWebhookRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sended = table.Column<bool>(type: "bit", nullable: false),
                    SendCount = table.Column<int>(type: "int", nullable: false),
                    CoinType = table.Column<int>(type: "int", nullable: false),
                    TronId = table.Column<int>(type: "int", nullable: true),
                    BitcoinId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblWebhookRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblWebhookRequests_TblBitcoins_BitcoinId",
                        column: x => x.BitcoinId,
                        principalTable: "TblBitcoins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TblWebhookRequests_TblTrons_TronId",
                        column: x => x.TronId,
                        principalTable: "TblTrons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblBitcoins_WalletId",
                table: "TblBitcoins",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_TblSellRequests_WalletId",
                table: "TblSellRequests",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_TblTrons_WalletId",
                table: "TblTrons",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_TblWebhookRequests_BitcoinId",
                table: "TblWebhookRequests",
                column: "BitcoinId");

            migrationBuilder.CreateIndex(
                name: "IX_TblWebhookRequests_TronId",
                table: "TblWebhookRequests",
                column: "TronId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblErrors");

            migrationBuilder.DropTable(
                name: "TblSellRequests");

            migrationBuilder.DropTable(
                name: "TblWebhookRequests");

            migrationBuilder.DropTable(
                name: "TblBitcoins");

            migrationBuilder.DropTable(
                name: "TblTrons");

            migrationBuilder.DropTable(
                name: "TblWallets");
        }
    }
}
