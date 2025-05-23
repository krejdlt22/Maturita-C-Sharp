using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        [Required(ErrorMessage = "Heslo je povinné")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
