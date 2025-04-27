using System;
using System.Collections.Generic;

namespace Pletko.Models;

public partial class UserStatus
{
    public int StatusId { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<Korisnik> Korisniks { get; set; } = new List<Korisnik>();
}
