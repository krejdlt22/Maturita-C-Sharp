using System.ComponentModel.DataAnnotations;

namespace Maturita_C_.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public bool ConsentGiven { get; set; }
    }
}