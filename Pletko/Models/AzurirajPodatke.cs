using System.ComponentModel.DataAnnotations;

namespace Pletko.Models
{
    public class AzurirajPodatke
    {
        public int KorisnikId { get; set; }
        [Required(ErrorMessage = "Mora te uneti ime.")]
        public string Ime { get; set; } = null!;
        [Required(ErrorMessage = "Mora te uneti prezime.")]
        public string Prezime { get; set; } = null!;
        [Required(ErrorMessage = "Mora te uneti email.")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Mora te uneti adresu.")]
        public string Adresa { get; set; } = null!;

        [Required(ErrorMessage = "Morate uneti vašu lozinku.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;

        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Mora te uneti broj telefona.")]
        public string BrojTelefona { get; set; } = null!;
    }
}
