using System.ComponentModel.DataAnnotations;

namespace Pletko.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Morate uneti vašu e-mail adresu.")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Morate uneti vašu lozinku.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
