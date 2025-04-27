using System.ComponentModel.DataAnnotations;

namespace Pletko.Models
{
    public class ProizvodBO
    {
        public int ProizvodId { get; set; }
        [Required(ErrorMessage = "Morate uneti ime proizvoda.")]
        public string Naziv { get; set; } = null!;
        [Required(ErrorMessage = "Morate uneti cenu proizvoda.")]
        public decimal Cena { get; set; }
        [Required(ErrorMessage = "Morate uneti količinu prouzvoda.")]
        public int Kolicina { get; set; }
        [Required(ErrorMessage = "Morate uneti opis proizvoda.")]
        public string Opis { get; set; } = null!;
        [Required(ErrorMessage = "Morate odabrati kategoriju.")]

        public int KategorijaId { get; set; }

        [Required(ErrorMessage = "Morate uneti ID korisnika.")]
        public int? KorisnikId { get; set; }
        [Required(ErrorMessage = "Unesite vašu lozinku.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;
        public  KorisnikBO? Korisnik { get; set; }
        public  KategorijaBO Kategorija { get; set; } = null!;
    }
}
