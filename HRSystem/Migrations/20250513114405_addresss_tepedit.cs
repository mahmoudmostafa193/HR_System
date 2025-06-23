using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Migrations
{
    /// <inheritdoc />
    public partial class addresss_tepedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BindingAddress_AddressId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BindingAddress_BindingAddressAddressId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "BindingAddress");

            migrationBuilder.RenameColumn(
                name: "BindingAddressAddressId",
                table: "Employees",
                newName: "EmployeeAddressAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_BindingAddressAddressId",
                table: "Employees",
                newName: "IX_Employees_EmployeeAddressAddressId");

            migrationBuilder.CreateTable(
                name: "EmployeeAddress",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAddress", x => x.AddressId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeAddress_AddressId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeAddress_EmployeeAddressAddressId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeAddress");

            migrationBuilder.RenameColumn(
                name: "EmployeeAddressAddressId",
                table: "Employees",
                newName: "BindingAddressAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeAddressAddressId",
                table: "Employees",
                newName: "IX_Employees_BindingAddressAddressId");

            migrationBuilder.CreateTable(
                name: "BindingAddress",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BindingAddress", x => x.AddressId);
                });

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
    }
}
