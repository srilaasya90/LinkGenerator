using System.ComponentModel.DataAnnotations;

namespace LinkExpiry.Models
{
    public class LinkRequest
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Username contains invalid characters.")]
        public string Username { get; set; }


    }
}
