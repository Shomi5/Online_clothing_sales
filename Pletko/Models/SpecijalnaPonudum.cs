using System;
using System.Collections.Generic;

namespace Pletko.Models;

public partial class SpecijalnaPonudum
{
    public int PonudaId { get; set; }

    public string Naziv { get; set; } = null!;

    public decimal Popust { get; set; }

    public string Opis { get; set; } = null!;

    public int KorisnikId { get; set; }

    public virtual ICollection<Korisnik> Korisniks { get; set; } = new List<Korisnik>();
}
