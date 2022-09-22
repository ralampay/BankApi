using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApi.Migrations
{
    public partial class ModifyUniquePINCodeAndCardNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ATMCards_CardNumber",
                table: "ATMCards");

            migrationBuilder.DropIndex(
                name: "IX_ATMCards_PINCode",
                table: "ATMCards");

            migrationBuilder.CreateIndex(
                name: "IX_ATMCards_PINCode_CardNumber",
                table: "ATMCards",
                columns: new[] { "PINCode", "CardNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ATMCards_PINCode_CardNumber",
                table: "ATMCards");

            migrationBuilder.CreateIndex(
                name: "IX_ATMCards_CardNumber",
                table: "ATMCards",
                column: "CardNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ATMCards_PINCode",
                table: "ATMCards",
                column: "PINCode",
                unique: true);
        }
    }
}
