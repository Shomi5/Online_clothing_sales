namespace Pletko.Models
{
    public class NarudzbinaBO
    {
        public int NarudzbinaId { get; set; }

        public DateTime DatumNarudzbine { get; set; }
        public string? BrojKartice { get; set; }
        public int KorisnikId { get; set; }

        public int ProizvodId { get; set; }

        public int Kolicina { get; set; }

        public decimal? UkupnaCena { get; set; }
        public KorisnikBO Korisnik { get; set; } = null!;

        public ProizvodBO Proizvod { get; set; } = null!;
    }
}
