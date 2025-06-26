using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqliteConnector.Migrations
{
    /// <inheritdoc />
    public partial class addlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Layers",
                table: "ArchitectureEnts",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Layers",
                table: "ArchitectureEnts");
        }
    }
}
