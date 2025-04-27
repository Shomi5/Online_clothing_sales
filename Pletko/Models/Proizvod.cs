using System;
using System.Collections.Generic;

namespace Pletko.Models;

public partial class Proizvod
{
    public int ProizvodId { get; set; }

    public string Naziv { get; set; } = null!;

    public decimal Cena { get; set; }

    public int Kolicina { get; set; }

    public string Opis { get; set; } = null!;

    public int? KorisnikId { get; set; }

    public int KategorijaId { get; set; }

    public virtual Kategorija Kategorija { get; set; } = null!;

    public virtual ICollection<Narudzbina> Narudzbinas { get; set; } = new List<Narudzbina>();
}
