using System.ComponentModel.DataAnnotations;

namespace Pletko.Models
{
    public class KorisnikBO
    {
        public int KorisnikId { get; set; }

        public string Ime { get; set; } = null!;

        public string Prezime { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Adresa { get; set; } = null!;

        public DateOnly DatumRodjenja { get; set; }

        public string? Password { get; set; }

        public string BrojTelefona { get; set; } = null!;

        public int StatusId { get; set; }
        public  SpecijalnaPonudaBO? Ponuda { get; set; }
        public UserStatusBO Status { get; set; } = null!;
    }
}
