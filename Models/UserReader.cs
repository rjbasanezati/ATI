using System.ComponentModel.DataAnnotations;

namespace ATI_IEC.Models
{
    public class UserReader
    {
        public int Id { get; set; }

        [Required] public string Name { get; set; } = "";
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
        [Required] public string Office { get; set; } = "";
        [Required, Phone] public string ContactNumber { get; set; } = "";
    }
}
