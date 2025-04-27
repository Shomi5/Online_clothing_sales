using System;
using System.Collections.Generic;

namespace Pletko.Models;

public partial class KupovinaPonude
{
    public int PonudaId { get; set; }

    public int KoriniskId { get; set; }

    public DateTime VremeKupovine { get; set; }

    public virtual Korisnik Korinisk { get; set; } = null!;

    public virtual SpecijalnaPonudum Ponuda { get; set; } = null!;
}
