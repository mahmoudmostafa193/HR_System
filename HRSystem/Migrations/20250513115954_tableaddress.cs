using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Migrations
{
    /// <inheritdoc />
    public partial class tableaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeAddress_AddressId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeAddress_EmployeeAddressAddressId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeAddress",
                table: "EmployeeAddress");

            migrationBuilder.RenameTable(
                name: "EmployeeAddress",
                newName: "EmployeeAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeAddresses",
                table: "EmployeeAddresses",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeAddresses_AddressId",
                table: "Employees",
                column: "AddressId",
                principalTable: "EmployeeAddresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeAddresses_EmployeeAddressAddressId",
                table: "Employees",
                column: "EmployeeAddressAddressId",
                principalTable: "EmployeeAddresses",
                principalColumn: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeAddresses_AddressId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeAddresses_EmployeeAddressAddressId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeAddresses",
                table: "EmployeeAddresses");

            migrationBuilder.RenameTable(
                name: "EmployeeAddresses",
                newName: "EmployeeAddress");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeAddress",
                table: "EmployeeAddress",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeAddress_AddressId",
                table: "Employees",
                column: "AddressId",
                principalTable: "EmployeeAddress",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeAddress_EmployeeAddressAddressId",
                table: "Employees",
                column: "EmployeeAddressAddressId",
                principalTable: "EmployeeAddress",
                principalColumn: "AddressId");
        }
    }
}
