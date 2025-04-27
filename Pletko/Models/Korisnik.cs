using System;
using System.Collections.Generic;

namespace Pletko.Models;

public partial class Korisnik
{
    public int KorisnikId { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Adresa { get; set; } = null!;

    public DateOnly DatumRodjenja { get; set; }

    public string Password { get; set; } = null!;

    public string BrojTelefona { get; set; } = null!;

    public int StatusId { get; set; }

    public int? PonudaId { get; set; }

    public virtual ICollection<Narudzbina> Narudzbinas { get; set; } = new List<Narudzbina>();

    public virtual SpecijalnaPonudum? Ponuda { get; set; }

    public virtual UserStatus Status { get; set; } = null!;
}
