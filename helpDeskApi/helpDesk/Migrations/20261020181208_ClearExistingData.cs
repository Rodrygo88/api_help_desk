using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace helpDesk.Migrations
{
    /// <inheritdoc />
    public partial class ClearExistingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM comments;");
            migrationBuilder.Sql("DELETE FROM tickets;");
            migrationBuilder.Sql("DELETE FROM users;");
            migrationBuilder.Sql("DELETE FROM sqlite_sequence WHERE name IN ('comments', 'tickets', 'users');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // The deleted records cannot be restored by rolling back this migration.
        }
    }
}
