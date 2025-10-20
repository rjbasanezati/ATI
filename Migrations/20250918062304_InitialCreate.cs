using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATI_IEC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IecDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IecDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpsRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Organization = table.Column<string>(type: "TEXT", nullable: false),
                    RequestDetails = table.Column<string>(type: "TEXT", nullable: false),
                    PdfPath = table.Column<string>(type: "TEXT", nullable: true),
                    Pen = table.Column<bool>(type: "INTEGER", nullable: false),
                    PenQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Bag = table.Column<bool>(type: "INTEGER", nullable: false),
                    BagQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Fan = table.Column<bool>(type: "INTEGER", nullable: false),
                    FanQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Notebook = table.Column<bool>(type: "INTEGER", nullable: false),
                    NotebookQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Notepad = table.Column<bool>(type: "INTEGER", nullable: false),
                    NotepadQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Certificate = table.Column<bool>(type: "INTEGER", nullable: false),
                    CertificateQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Program = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProgramQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Banner = table.Column<bool>(type: "INTEGER", nullable: false),
                    BannerQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CallingCard = table.Column<bool>(type: "INTEGER", nullable: false),
                    CallingCardQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Book = table.Column<bool>(type: "INTEGER", nullable: false),
                    BookQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Manual = table.Column<bool>(type: "INTEGER", nullable: false),
                    ManualQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Report = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReportQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    FullColor = table.Column<bool>(type: "INTEGER", nullable: false),
                    FullColorQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    BlackWhite = table.Column<bool>(type: "INTEGER", nullable: false),
                    BlackWhiteQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Cutting = table.Column<bool>(type: "INTEGER", nullable: false),
                    CuttingQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Sorting = table.Column<bool>(type: "INTEGER", nullable: false),
                    SortingQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    PerfectBinding = table.Column<bool>(type: "INTEGER", nullable: false),
                    PerfectBindingQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    RingBinding = table.Column<bool>(type: "INTEGER", nullable: false),
                    RingBindingQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Folding = table.Column<bool>(type: "INTEGER", nullable: false),
                    FoldingQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Boxing = table.Column<bool>(type: "INTEGER", nullable: false),
                    BoxingQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ISSN = table.Column<bool>(type: "INTEGER", nullable: false),
                    ISBN = table.Column<bool>(type: "INTEGER", nullable: false),
                    VideoRecording = table.Column<bool>(type: "INTEGER", nullable: false),
                    VideoStreaming = table.Column<bool>(type: "INTEGER", nullable: false),
                    PhotoCoverage = table.Column<bool>(type: "INTEGER", nullable: false),
                    AudioRecording = table.Column<bool>(type: "INTEGER", nullable: false),
                    VideoSoundSetup = table.Column<bool>(type: "INTEGER", nullable: false),
                    Others = table.Column<string>(type: "TEXT", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpsRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserReaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Barangay = table.Column<string>(type: "TEXT", nullable: false),
                    Agency = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReaders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IecDocuments");

            migrationBuilder.DropTable(
                name: "KpsRequests");

            migrationBuilder.DropTable(
                name: "UserReaders");
        }
    }
}
