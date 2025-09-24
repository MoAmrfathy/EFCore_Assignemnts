using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class update_companydb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "EmpName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmpName",
                table: "Employees",
                newName: "Name");
        }
    }
}
