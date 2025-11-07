using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IecDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpsRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Organization = table.Column<string>(type: "text", nullable: false),
                    RequestDetails = table.Column<string>(type: "text", nullable: false),
                    PdfPath = table.Column<string>(type: "text", nullable: true),
                    Pen = table.Column<bool>(type: "boolean", nullable: false),
                    PenQuantity = table.Column<int>(type: "integer", nullable: false),
                    Bag = table.Column<bool>(type: "boolean", nullable: false),
                    BagQuantity = table.Column<int>(type: "integer", nullable: false),
                    Fan = table.Column<bool>(type: "boolean", nullable: false),
                    FanQuantity = table.Column<int>(type: "integer", nullable: false),
                    Notebook = table.Column<bool>(type: "boolean", nullable: false),
                    NotebookQuantity = table.Column<int>(type: "integer", nullable: false),
                    Notepad = table.Column<bool>(type: "boolean", nullable: false),
                    NotepadQuantity = table.Column<int>(type: "integer", nullable: false),
                    Certificate = table.Column<bool>(type: "boolean", nullable: false),
                    CertificateQuantity = table.Column<int>(type: "integer", nullable: false),
                    Program = table.Column<bool>(type: "boolean", nullable: false),
                    ProgramQuantity = table.Column<int>(type: "integer", nullable: false),
                    Banner = table.Column<bool>(type: "boolean", nullable: false),
                    BannerQuantity = table.Column<int>(type: "integer", nullable: false),
                    CallingCard = table.Column<bool>(type: "boolean", nullable: false),
                    CallingCardQuantity = table.Column<int>(type: "integer", nullable: false),
                    Book = table.Column<bool>(type: "boolean", nullable: false),
                    BookQuantity = table.Column<int>(type: "integer", nullable: false),
                    Manual = table.Column<bool>(type: "boolean", nullable: false),
                    ManualQuantity = table.Column<int>(type: "integer", nullable: false),
                    Report = table.Column<bool>(type: "boolean", nullable: false),
                    ReportQuantity = table.Column<int>(type: "integer", nullable: false),
                    FullColor = table.Column<bool>(type: "boolean", nullable: false),
                    FullColorQuantity = table.Column<int>(type: "integer", nullable: false),
                    BlackWhite = table.Column<bool>(type: "boolean", nullable: false),
                    BlackWhiteQuantity = table.Column<int>(type: "integer", nullable: false),
                    Cutting = table.Column<bool>(type: "boolean", nullable: false),
                    CuttingQuantity = table.Column<int>(type: "integer", nullable: false),
                    Sorting = table.Column<bool>(type: "boolean", nullable: false),
                    SortingQuantity = table.Column<int>(type: "integer", nullable: false),
                    PerfectBinding = table.Column<bool>(type: "boolean", nullable: false),
                    PerfectBindingQuantity = table.Column<int>(type: "integer", nullable: false),
                    RingBinding = table.Column<bool>(type: "boolean", nullable: false),
                    RingBindingQuantity = table.Column<int>(type: "integer", nullable: false),
                    Folding = table.Column<bool>(type: "boolean", nullable: false),
                    FoldingQuantity = table.Column<int>(type: "integer", nullable: false),
                    Boxing = table.Column<bool>(type: "boolean", nullable: false),
                    BoxingQuantity = table.Column<int>(type: "integer", nullable: false),
                    ISSN = table.Column<bool>(type: "boolean", nullable: false),
                    ISBN = table.Column<bool>(type: "boolean", nullable: false),
                    VideoRecording = table.Column<bool>(type: "boolean", nullable: false),
                    VideoStreaming = table.Column<bool>(type: "boolean", nullable: false),
                    PhotoCoverage = table.Column<bool>(type: "boolean", nullable: false),
                    AudioRecording = table.Column<bool>(type: "boolean", nullable: false),
                    VideoSoundSetup = table.Column<bool>(type: "boolean", nullable: false),
                    Others = table.Column<string>(type: "text", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpsRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserReaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Office = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: false)
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
