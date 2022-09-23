using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApi.Migrations
{
    public partial class AddedCustomerIdToATMCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ATMCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ATMCards_CustomerId",
                table: "ATMCards",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ATMCards_Customers_CustomerId",
                table: "ATMCards",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMCards_Customers_CustomerId",
                table: "ATMCards");

            migrationBuilder.DropIndex(
                name: "IX_ATMCards_CustomerId",
                table: "ATMCards");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ATMCards");
        }
    }
}
