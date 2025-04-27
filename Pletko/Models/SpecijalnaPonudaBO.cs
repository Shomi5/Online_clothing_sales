using System.ComponentModel.DataAnnotations;

namespace Pletko.Models
{
    public class SpecijalnaPonudaBO
    {
        public int PonudaId { get; set; }
        [Required(ErrorMessage = "Morate uneti naziv proizvoda.")]
        public string Naziv { get; set; } = null!;
        [Required(ErrorMessage = "Popust je obavezno polje.")]
        [Range(5, 80, ErrorMessage = "Procenat popusta mora biti između 5% i 80%.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Popust { get; set; }
        [Required(ErrorMessage = "Morate uneti opis proizvoda.")]
        public string Opis { get; set; } = null!;
        [Required(ErrorMessage = "Morate uneti ID korisnika.")]
        public int KorisnikId { get; set; }

        [Required(ErrorMessage = "Unesite vašu lozinku.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;
    }
}
