using System.ComponentModel.DataAnnotations;

namespace Pletko.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Morate uneti vaše ime.")]
        public string Ime { get; set; } = null!;
        [Required(ErrorMessage = "Morate uneti vaše prezime.")]
        public string Prezime { get; set; } = null!;

        [Required(ErrorMessage = "Morate uneti vašu e-mail adresu.")]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Morate uneti vašu adresu.")]
        public string Adresa { get; set; } = null!;
        [Required(ErrorMessage = "Morate uneti vaš datum rodjenja.")]
        public DateOnly DatumRodjenja { get; set; }

        [Required(ErrorMessage = "Morate uneti vašu lozinku.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lozinke se ne poklapaju.")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Morate uneti vaš broj telefona.")]
        public string BrojTelefona { get; set; } = null!;
    }
}
