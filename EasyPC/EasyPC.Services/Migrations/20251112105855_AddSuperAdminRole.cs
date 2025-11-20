using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPC.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddSuperAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert initial SuperAdmin user
            // Role: 3 = SuperAdmin, IsDeleted: 0 = false
            migrationBuilder.Sql(@"
                INSERT INTO Users (Username, Password, Email, FirstName, LastName, Role, IsDeleted)
                VALUES ('superadmin', 'superadmin123', 'superadmin@easypc.com', 'Super', 'Admin', 3, 0)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
