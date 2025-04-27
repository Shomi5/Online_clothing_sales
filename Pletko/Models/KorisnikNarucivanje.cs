using System.ComponentModel.DataAnnotations;

namespace Pletko.Models
{
    public class KorisnikNarucivanje
    {
        [Required(ErrorMessage = "Morate uneti podatke o kartici.")]
        [RegularExpression(@"^[0-9]{4}\ [0-9]{4}\ [0-9]{4}\ [0-9]{4}$", ErrorMessage = "Morate uneti pravilno broj kartice: xxxx xxxx xxxx xxxx.")]
        public string? BrojKartice { get; set; }

        [Required(ErrorMessage = "Morate uneti e-mail adresu.")]
        [EmailAddress]
        public string Email { get; set; } = null!;
        public int ProizvodID { get; set; }

        public decimal? Popust { get; set; }

        [Required(ErrorMessage = "Morate uneti količinu.")]
        [Range(1, int.MaxValue, ErrorMessage = "Količina mora biti najmanje 1.")]
        public int Kolicina { get; set; }
    }
}
