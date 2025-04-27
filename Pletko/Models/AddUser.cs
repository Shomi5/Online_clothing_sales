using System.ComponentModel.DataAnnotations;

namespace Pletko.Models
{
    public class AddUser
    {
        [Required(ErrorMessage = "Morate uneti korisničko ime.")]
        public string Ime { get; set; } = null!;
        [Required(ErrorMessage = "Morate uneti korisničko prezime.")]
        public string Prezime { get; set; } = null!;

        [Required(ErrorMessage = "Morate uneti korisničku e-mail adresu.")]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Morate uneti kornisničku adresu.")]
        public string Adresa { get; set; } = null!;
        [Required(ErrorMessage = "Morate uneti korisnički datum rodjenja.")]
        public DateOnly DatumRodjenja { get; set; }

        [Required(ErrorMessage = "Unesite vašu lozinku.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "Morate uneti korisničku lozinku.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lozinke se ne poklapaju.")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Morate uneti korisnički broj telefona.")]
        public string BrojTelefona { get; set; } = null!;
        [Required(ErrorMessage = "Morate odabrati status.")]
        [Range(1, int.MaxValue, ErrorMessage = "Morate izabrati status korisnika.")]
        public int StatusId { get; set; }
    }
}
