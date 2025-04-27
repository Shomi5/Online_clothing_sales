using System;
using System.Collections.Generic;

namespace Pletko.Models;

public partial class Narudzbina
{
    public int NarudzbinaId { get; set; }

    public DateTime DatumNarudzbine { get; set; }

    public string BrojKartice { get; set; } = null!;

    public int KorisnikId { get; set; }

    public int ProizvodId { get; set; }

    public int Kolicina { get; set; }

    public decimal? UkupnaCena { get; set; }

    public virtual Korisnik Korisnik { get; set; } = null!;

    public virtual Proizvod Proizvod { get; set; } = null!;
}
