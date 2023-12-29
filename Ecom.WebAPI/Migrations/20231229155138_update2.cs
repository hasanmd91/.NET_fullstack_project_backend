using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_details_order_order_id",
                table: "order_details");

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_order_order_id",
                table: "order_details",
                column: "order_id",
                principalTable: "order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_details_order_order_id",
                table: "order_details");

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_order_order_id",
                table: "order_details",
                column: "order_id",
                principalTable: "order",
                principalColumn: "id");
        }
    }
}
