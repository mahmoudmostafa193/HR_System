using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Migrations
{
    /// <inheritdoc />
    public partial class addressstepedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BindingAddress_AddressId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BindingAddress",
                newName: "AddressId");

            migrationBuilder.AddColumn<int>(
                name: "BindingAddressAddressId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BindingAddressAddressId",
                table: "Employees",
                column: "BindingAddressAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BindingAddress_AddressId",
                table: "Employees",
                column: "AddressId",
                principalTable: "BindingAddress",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BindingAddress_BindingAddressAddressId",
                table: "Employees",
                column: "BindingAddressAddressId",
                principalTable: "BindingAddress",
                principalColumn: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BindingAddress_AddressId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BindingAddress_BindingAddressAddressId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BindingAddressAddressId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BindingAddressAddressId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "BindingAddress",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BindingAddress_AddressId",
                table: "Employees",
                column: "AddressId",
                principalTable: "BindingAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
