using Microsoft.EntityFrameworkCore;
using Pletko.Models.Interface;

namespace Pletko.Models.EFRepository
{
    public class ModeratorRepository : IModeratorRepository
    {
        private readonly PletkoContext _context = new PletkoContext();

        public bool DodajPonudu(SpecijalnaPonudaBO novaPonuda)
        {
            var dodajPonudu = new SpecijalnaPonudum()
            {
                Naziv = novaPonuda.Naziv,
                Popust = novaPonuda.Popust,
                Opis = novaPonuda.Opis,
                KorisnikId = novaPonuda.KorisnikId,
            };

            if (dodajPonudu != null)
            {
                _context.SpecijalnaPonuda.Add(dodajPonudu);

                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            else
            {
                return false;
            }
        }



        public SpecijalnaPonudaBO nadjiPonudu(int ponudaId)
        {
            SpecijalnaPonudum? ponuda = _context.SpecijalnaPonuda.SingleOrDefault(p => p.PonudaId == ponudaId);
            SpecijalnaPonudaBO traziPonudu = new SpecijalnaPonudaBO()
            {
                PonudaId = ponuda.PonudaId,
                Naziv = ponuda.Naziv,
                Popust = ponuda.Popust,
                Opis = ponuda.Opis,
                KorisnikId = ponuda.KorisnikId
            };

            return traziPonudu;
        }

        public bool EditPonude(SpecijalnaPonudaBO editPonuda)
        {
            var provera = _context.SpecijalnaPonuda.SingleOrDefault(x => x.PonudaId == editPonuda.PonudaId);

            if (provera == null)
            {
                return false; 
            }

            _context.SpecijalnaPonuda
                .Where(x => x.PonudaId == editPonuda.PonudaId)
                .ExecuteUpdate(setters => setters
                    .SetProperty(x => x.Naziv, editPonuda.Naziv)
                    .SetProperty(x => x.Popust, editPonuda.Popust)
                    .SetProperty(x => x.Opis, editPonuda.Opis)
                    .SetProperty(x => x.KorisnikId, editPonuda.KorisnikId)

                );

            return true;
        }

        public IEnumerable<SpecijalnaPonudaBO> SvePonude()
        {
            List<SpecijalnaPonudaBO> ponude = new List<SpecijalnaPonudaBO>();

            foreach(SpecijalnaPonudum pon in _context.SpecijalnaPonuda.ToList())
            {
                SpecijalnaPonudaBO ponuda = new SpecijalnaPonudaBO()
                {
                    PonudaId = pon.PonudaId,
                    Naziv = pon.Naziv,
                    Popust = pon.Popust,
                    Opis = pon.Opis,
                    KorisnikId = pon.KorisnikId,
                };

                ponude.Add(ponuda);
            }

            return ponude;
        }

        public IEnumerable<KorisnikBO> SviKorisnici()
        {
            List<KorisnikBO> korisnici = new List<KorisnikBO>();

            foreach (Korisnik kor in _context.Korisniks.ToList())
            {
                UserStatus? status = _context.UserStatuses.SingleOrDefault(x => x.StatusId == kor.StatusId);
                SpecijalnaPonudum? ponuda = _context.SpecijalnaPonuda.SingleOrDefault(x => x.PonudaId == kor.PonudaId);


                if (status.Naziv != "Admin")
                {
                    if(ponuda != null && ponuda.PonudaId > 0)
                    {
                        KorisnikBO korisnik = new KorisnikBO()
                        {
                            KorisnikId = kor.KorisnikId,
                            Ime = kor.Ime,
                            Prezime = kor.Prezime,
                            Email = kor.Email,
                            Adresa = kor.Adresa,
                            DatumRodjenja = kor.DatumRodjenja,
                            Password = kor.Password,
                            BrojTelefona = kor.BrojTelefona,
                            StatusId = kor.StatusId,
                            Status = new UserStatusBO()
                            {
                                StatusId = status.StatusId,
                                Naziv = status.Naziv,
                            },
                            Ponuda = new SpecijalnaPonudaBO()
                            {
                                PonudaId = ponuda.PonudaId,
                                Naziv = ponuda.Naziv,
                                Popust = ponuda.Popust,
                                Opis= ponuda.Opis,
                                KorisnikId = ponuda.KorisnikId
                            } 

                        };

                        korisnici.Add(korisnik);
                    }
                    else
                    {
                        KorisnikBO korisnik = new KorisnikBO()
                        {
                            KorisnikId = kor.KorisnikId,
                            Ime = kor.Ime,
                            Prezime = kor.Prezime,
                            Email = kor.Email,
                            Adresa = kor.Adresa,
                            DatumRodjenja = kor.DatumRodjenja,
                            Password = kor.Password,
                            BrojTelefona = kor.BrojTelefona,
                            StatusId = kor.StatusId,
                            Status = new UserStatusBO()
                            {
                                StatusId = status.StatusId,
                                Naziv = status.Naziv,
                            },
                            Ponuda = new SpecijalnaPonudaBO()
                            {
                                PonudaId = 0,
                                Naziv = "Nema",
                                Popust = 0,
                                Opis = "Nema",
                                KorisnikId = 0
                            }
                        };

                        korisnici.Add(korisnik);
                    }
                   
                }
            }

            return korisnici;
        }

        public bool ObrisiPonudu(int PonudaID)
        {
            var ponuda = _context.SpecijalnaPonuda.FirstOrDefault(p => p.PonudaId == PonudaID);

            if (ponuda != null)
            {
                _context.SpecijalnaPonuda.Remove(ponuda);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        public bool DodeliPonudu(SPKorisnika ponudaKorisnika)
        {
            var provera = _context.SpecijalnaPonuda.SingleOrDefault(x => x.PonudaId == ponudaKorisnika.PonudaId);

            if (provera == null)
            {
                return false;
            }

            _context.Korisniks
                .Where(x => x.KorisnikId == ponudaKorisnika.KorisnikId)
                .ExecuteUpdate(setters => setters
                    .SetProperty(x => x.PonudaId, ponudaKorisnika.PonudaId)
                );

            return true;
        }

        public bool OtkaziPonudu(int KorisnikID)
        {
            var provera = _context.Korisniks.SingleOrDefault(x => x.KorisnikId == KorisnikID);

            if (provera == null)
            {
                return false;
            }

            _context.Korisniks
                .Where(x => x.KorisnikId == provera.KorisnikId)
                .ExecuteUpdate(setters => setters
                    .SetProperty(x => x.PonudaId, (int?)null)
                );

            return true;
        }
    }
}
