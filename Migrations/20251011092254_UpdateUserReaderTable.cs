using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATI_IEC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserReaderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Barangay",
                table: "UserReaders",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Agency",
                table: "UserReaders",
                newName: "Office");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserReaders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserReaders");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserReaders",
                newName: "Barangay");

            migrationBuilder.RenameColumn(
                name: "Office",
                table: "UserReaders",
                newName: "Agency");
        }
    }
}
