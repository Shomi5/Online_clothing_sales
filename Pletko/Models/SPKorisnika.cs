using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Pletko.Models
{
    public class SPKorisnika
    {
        [Required(ErrorMessage ="Morate uneti korisnički ID.")]
        public int KorisnikId { get; set; }
        [Required(ErrorMessage = "Morate izabrato ponudu.")]
        [Range(0, int.MaxValue, ErrorMessage = "Morate izabrati ponudu!")]
        public int PonudaId { get; set; }

        [Required(ErrorMessage = "Unesite vašu lozinku.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;
    }
}
