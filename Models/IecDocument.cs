using System;
using System.ComponentModel.DataAnnotations;

namespace ATI_IEC.Models
{
    public class IecDocument
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }  // Nullable to avoid errors

        [Required]
        public string FilePath { get; set; } = null!;

        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
