using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatementViewer.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    StartOfPeriod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndOfPeriod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GenerationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Сurrency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountUnit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StatementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountUnit_Statement_StatementId",
                        column: x => x.StatementId,
                        principalTable: "Statement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Asset = table.Column<decimal>(type: "decimal(30,5)", precision: 30, scale: 5, nullable: false),
                    Passive = table.Column<decimal>(type: "decimal(30,5)", precision: 30, scale: 5, nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(30,5)", precision: 30, scale: 5, nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(30,5)", precision: 30, scale: 5, nullable: false),
                    AccountUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountUnit_AccountUnitId",
                        column: x => x.AccountUnitId,
                        principalTable: "AccountUnit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountUnitId",
                table: "Account",
                column: "AccountUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUnit_StatementId",
                table: "AccountUnit",
                column: "StatementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "AccountUnit");

            migrationBuilder.DropTable(
                name: "Statement");
        }
    }
}
