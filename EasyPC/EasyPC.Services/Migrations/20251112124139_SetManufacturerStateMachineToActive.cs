using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPC.Services.Migrations
{
    /// <inheritdoc />
    public partial class SetManufacturerStateMachineToActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update all manufacturers with null or empty StateMachine to 'active'
            migrationBuilder.Sql(@"
                UPDATE Manufacturers
                SET StateMachine = 'active'
                WHERE StateMachine IS NULL OR StateMachine = ''
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Optionally revert the change (set back to null)
            migrationBuilder.Sql(@"
                UPDATE Manufacturers
                SET StateMachine = NULL
                WHERE StateMachine = 'active'
            ");
        }
    }
}
