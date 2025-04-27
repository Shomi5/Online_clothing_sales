using System.ComponentModel.DataAnnotations;

namespace Pletko.Models
{
    public class DodajProizvod
    {
        [Required(ErrorMessage = "Morate uneti naziv proizvoda.")]
        public string Naziv { get; set; } = null!;
        [Range(500, double.MaxValue, ErrorMessage = "Cena mora biti najmanje 500 dinara.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Cena { get; set; }
        [Required(ErrorMessage = "Morate uneti količinu proizvoda.")]
        [Range(1, int.MaxValue, ErrorMessage = "Količina mora biti najmanje 1.")]
        public int Kolicina { get; set; }
        [Required(ErrorMessage = "Morate uneti opis proizvoda.")]
        public string Opis { get; set; } = null!;

        [Required(ErrorMessage = "Morate odabrati kategoriju.")]
        [Range(1, int.MaxValue, ErrorMessage = "Morate izabrati kategoriju proizvoda.")]
        public int KategorijaId { get; set; }

        [Required(ErrorMessage = "Morate uneti ID korisnika.")]
        public int? KorisnikId { get; set; }
        [Required(ErrorMessage = "Unesite vašu lozinku.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;
        [Required(ErrorMessage = "Morate odabrati sliku proizvoda.")]
        public IFormFile? SlikaArtikla { get; set; } = null!;
    }
}
