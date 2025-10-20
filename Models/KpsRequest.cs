using System;
using System.ComponentModel.DataAnnotations;

namespace ATI_IEC.Models
{
    public class KpsRequest
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } // the reader submitting

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Organization { get; set; } = string.Empty;

        [Display(Name = "Request Details")]
        public string RequestDetails { get; set; } = string.Empty;

        public string? PdfPath { get; set; }

        // ---------------- CORPORATE MERCHANDISE ----------------
        public bool Pen { get; set; }
        public int PenQuantity { get; set; }

        public bool Bag { get; set; }
        public int BagQuantity { get; set; }

        public bool Fan { get; set; }
        public int FanQuantity { get; set; }

        public bool Notebook { get; set; }
        public int NotebookQuantity { get; set; }

        public bool Notepad { get; set; }
        public int NotepadQuantity { get; set; }

        // ---------------- PUBLICATIONS ----------------
        public bool Certificate { get; set; }
        public int CertificateQuantity { get; set; }

        public bool Program { get; set; }
        public int ProgramQuantity { get; set; }

        public bool Banner { get; set; }
        public int BannerQuantity { get; set; }

        public bool CallingCard { get; set; }
        public int CallingCardQuantity { get; set; }

        public bool Book { get; set; }
        public int BookQuantity { get; set; }

        public bool Manual { get; set; }
        public int ManualQuantity { get; set; }

        public bool Report { get; set; }
        public int ReportQuantity { get; set; }

        // ---------------- PRINTING ----------------
        public bool FullColor { get; set; }
        public int FullColorQuantity { get; set; }

        public bool BlackWhite { get; set; }
        public int BlackWhiteQuantity { get; set; }

        // ---------------- POST PROCESS ----------------
        public bool Cutting { get; set; }
        public int CuttingQuantity { get; set; }

        public bool Sorting { get; set; }
        public int SortingQuantity { get; set; }

        public bool PerfectBinding { get; set; }
        public int PerfectBindingQuantity { get; set; }

        public bool RingBinding { get; set; }
        public int RingBindingQuantity { get; set; }

        public bool Folding { get; set; }
        public int FoldingQuantity { get; set; }

        public bool Boxing { get; set; }
        public int BoxingQuantity { get; set; }

        // ---------------- ISSN/ISBN ----------------
        public bool ISSN { get; set; }
        public bool ISBN { get; set; }

        // ---------------- MEDIA COVERAGE ----------------
        public bool VideoRecording { get; set; }
        public bool VideoStreaming { get; set; }
        public bool PhotoCoverage { get; set; }
        public bool AudioRecording { get; set; }
        public bool VideoSoundSetup { get; set; }

        // ---------------- OTHERS ----------------
        [Display(Name = "Others")]
        public string Others { get; set; } = string.Empty;

        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
