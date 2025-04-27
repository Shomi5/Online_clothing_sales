using Microsoft.EntityFrameworkCore;
using Pletko.Models.Interface;

namespace Pletko.Models.EFRepository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly PletkoContext _context = new PletkoContext();

        public KorisnikBO NadjiKorisnika(int KorisnikID)
        {
            Korisnik? korisnik = _context.Korisniks.SingleOrDefault(p => p.KorisnikId == KorisnikID);
            SpecijalnaPonudum? ponuda = _context.SpecijalnaPonuda.SingleOrDefault(s => s.PonudaId == korisnik.PonudaId);

            if (ponuda != null && ponuda.PonudaId > 0)
            {
                KorisnikBO korisnikID = new KorisnikBO()
                {
                    KorisnikId = korisnik.KorisnikId,
                    Ime = korisnik.Ime,
                    Prezime = korisnik.Prezime,
                    Email = korisnik.Email,
                    Adresa = korisnik.Adresa,
                    DatumRodjenja = korisnik.DatumRodjenja,
                    Password = korisnik.Password,
                    BrojTelefona = korisnik.BrojTelefona,
                    StatusId = korisnik.StatusId,
                    Ponuda = new SpecijalnaPonudaBO()
                    {
                        PonudaId = ponuda.PonudaId,
                        Naziv = ponuda.Naziv,
                        Popust = ponuda.Popust,
                        Opis = ponuda.Opis,
                        KorisnikId = ponuda.KorisnikId
                    }

                };

                return korisnikID;
            }
            else
            {
                KorisnikBO korisnikID = new KorisnikBO()
                {
                    KorisnikId = korisnik.KorisnikId,
                    Ime = korisnik.Ime,
                    Prezime = korisnik.Prezime,
                    Email = korisnik.Email,
                    Adresa = korisnik.Adresa,
                    DatumRodjenja = korisnik.DatumRodjenja,
                    Password = korisnik.Password,
                    BrojTelefona = korisnik.BrojTelefona,
                    StatusId = korisnik.StatusId,
                    Ponuda = new SpecijalnaPonudaBO()
                    {
                        PonudaId = 0,
                        Naziv = "Nema",
                        Popust = 0,
                        Opis = "Nema",
                        KorisnikId = 0
                    }

                };

                return korisnikID;
            }

        }

        public IEnumerable<NarudzbinaBO> SveNarudzbe()
        {
            List<NarudzbinaBO> narudzbine = new List<NarudzbinaBO>();

            foreach(Narudzbina nar in _context.Narudzbinas.ToList())
            {

                Korisnik? korisnik = _context.Korisniks.FirstOrDefault(x=> x.KorisnikId == nar.KorisnikId);
                Proizvod? proizvod = _context.Proizvods.FirstOrDefault(p=> p.ProizvodId == nar.ProizvodId);
                NarudzbinaBO snar = new NarudzbinaBO()
                {
                    NarudzbinaId = nar.NarudzbinaId,
                    DatumNarudzbine = nar.DatumNarudzbine,
                    BrojKartice = nar.BrojKartice,
                    KorisnikId = nar.KorisnikId,
                    Kolicina = nar.Kolicina,
                    ProizvodId = nar.ProizvodId,
                    UkupnaCena = nar.UkupnaCena,
                    Korisnik = new KorisnikBO()
                    {
                        KorisnikId = korisnik.KorisnikId,
                        Ime = korisnik.Ime,
                        Prezime = korisnik.Prezime,
                        Email = korisnik.Email,
                        Adresa = korisnik.Adresa,
                        BrojTelefona = korisnik.BrojTelefona,
                    },
                    Proizvod = new ProizvodBO()
                    {
                        ProizvodId = proizvod.ProizvodId,
                        Naziv = proizvod.Naziv,
                        Cena = proizvod.Cena,
                        Kolicina = proizvod.Kolicina,
                        Opis = proizvod.Opis,
                        KorisnikId = proizvod.KorisnikId

                    }
                };

                narudzbine.Add(snar);
            }

            return narudzbine;
        }

        public IEnumerable<KorisnikBO> SviKorisnici()
        {
            List<KorisnikBO> korisnici = new List<KorisnikBO>();

            foreach (Korisnik kor in _context.Korisniks.ToList())
            {
                UserStatus? status = _context.UserStatuses.SingleOrDefault(x=>x.StatusId == kor.StatusId);

                if(status.Naziv != "Admin")
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
                        }
                    };

                    korisnici.Add(korisnik);
                }
            }

            return korisnici;
        }

        public IEnumerable<ProizvodBO> SviProizvodi()
        {
            List<ProizvodBO> proizvodi = new List<ProizvodBO>();

            foreach (Proizvod pro in _context.Proizvods.ToList())
            {

                Kategorija? kategorija = _context.Kategorijas.SingleOrDefault(x => x.KategorijaId == pro.KategorijaId);

                ProizvodBO sviProizvodi = new ProizvodBO()
                {
                    ProizvodId = pro.ProizvodId,
                    Naziv = pro.Naziv,
                    Cena = pro.Cena,
                    Kolicina = pro.Kolicina,
                    Opis = pro.Opis,
                    KorisnikId = pro.KorisnikId,
                    Kategorija = new KategorijaBO()
                    {
                        KategorijaId = kategorija.KategorijaId,
                        Naziv = kategorija.Naziv,
                    }
                };

                proizvodi.Add(sviProizvodi);
            }

            return proizvodi;
        }

        public IEnumerable<UserStatusBO> SviStatusi()
        {
            List<UserStatusBO> statusi = new List<UserStatusBO>();

            foreach(UserStatus status in _context.UserStatuses.ToList())
            {
                if(status.Naziv != "Admin")
                {
                    UserStatusBO sviStatusi = new UserStatusBO()
                    {
                        StatusId = status.StatusId,
                        Naziv = status.Naziv
                    };

                    statusi.Add(sviStatusi);
                }
            }

            return statusi;
        }
        public bool AzurirajPodatkeAdmin(AzurirajPodatke Korisnik)
        {

            var provera = _context.Korisniks.SingleOrDefault(x => x.KorisnikId == Korisnik.KorisnikId);

            if (provera == null)
            {
                return false; // Korisnik nije pronađen
            }

            // Ažuriranje samo promenjenih podataka
            _context.Korisniks
                .Where(x => x.KorisnikId == Korisnik.KorisnikId)
                .ExecuteUpdate(setters => setters
                    .SetProperty(x => x.Ime, Korisnik.Ime)
                    .SetProperty(x => x.Prezime, Korisnik.Prezime)
                    .SetProperty(x => x.Email, Korisnik.Email)
                    .SetProperty(x => x.Adresa, Korisnik.Adresa)
                    .SetProperty(x => x.DatumRodjenja, provera.DatumRodjenja)
                    .SetProperty(x => x.BrojTelefona, Korisnik.BrojTelefona)
                    .SetProperty(x => x.Password, Korisnik.Password ?? provera.Password) // Ako nije uneo novu šifru, zadržavamo staru
                );

            return true;

        }

        public bool OtkaziKorisnickuNarudzbinu(int NarudzbinaID)
        {
            var narudzbina = _context.Narudzbinas.FirstOrDefault(n => n.NarudzbinaId == NarudzbinaID);

            if (narudzbina != null)
            {
                _context.Narudzbinas.Remove(narudzbina);
                if(_context.SaveChanges() > 0)
                {
                    return true;
                }
                else { return false; }

                
            }
            else
            {
                return false;
            }
        }


        public bool DodajKorisnika(AddUser korisnik)
        {
            UserStatus? noviStatus = _context.UserStatuses.SingleOrDefault(s => s.StatusId == korisnik.StatusId);

            var noviKorisnik = new Korisnik
            {
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Email = korisnik.Email,
                Adresa = korisnik.Adresa,
                DatumRodjenja = korisnik.DatumRodjenja,
                Password = korisnik.Password,
                BrojTelefona = korisnik.BrojTelefona,
                StatusId = korisnik.StatusId,
            };

            if(noviKorisnik != null)
            {
                _context.Korisniks.Add(noviKorisnik);

                if(_context.SaveChanges() > 0)
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

        public bool ObrisiKorinika(int KorisnikID)
        {

            var korisnik = _context.Korisniks.FirstOrDefault(k => k.KorisnikId == KorisnikID);
            var provera = _context.Narudzbinas.FirstOrDefault(n => n.KorisnikId == korisnik.KorisnikId);
            if (korisnik != null)
            {
                if(provera == null)
                {
                    _context.Korisniks.Remove(korisnik);
                    if (_context.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
                
            }
            else
            {
                return false;
            }

        }

        public ProizvodBO NadjiProizvod(int ProizvodId)
        {
            Proizvod? trazeniProizvod = _context.Proizvods.SingleOrDefault(p=> p.ProizvodId == ProizvodId);
            Kategorija? kategorija = _context.Kategorijas.SingleOrDefault(k => k.KategorijaId == trazeniProizvod.KategorijaId);

            ProizvodBO proizovd = new ProizvodBO()
            {
                ProizvodId = trazeniProizvod.ProizvodId,
                Naziv = trazeniProizvod.Naziv,
                Cena = trazeniProizvod.Cena,
                Kolicina = trazeniProizvod.Kolicina,
                Opis = trazeniProizvod.Opis,
                KategorijaId = trazeniProizvod.KategorijaId,
                KorisnikId = trazeniProizvod.KorisnikId,
                Kategorija = new KategorijaBO()
                {
                    KategorijaId = kategorija.KategorijaId,
                    Naziv = kategorija.Naziv,
                }
            };

            return proizovd;
        }

        public IEnumerable<KategorijaBO> SveKategorije()
        {
            List<KategorijaBO> kategorije = new List<KategorijaBO>();

            foreach(Kategorija kat in _context.Kategorijas.ToList())
            {
                KategorijaBO kategorija = new KategorijaBO()
                {
                    KategorijaId = kat.KategorijaId,
                    Naziv = kat.Naziv
                };

                kategorije.Add(kategorija);
            }

            return kategorije;
        }

        public bool AzurirajPodatkeProizvoda(ProizovdEdit proizvod)
        {
            var provera = _context.Proizvods.SingleOrDefault(x => x.ProizvodId == proizvod.ProizvodId);

            if (provera == null)
            {
                return false; 
            }

            _context.Proizvods
                .Where(x => x.ProizvodId == proizvod.ProizvodId)
                .ExecuteUpdate(setters => setters
                    .SetProperty(x => x.Naziv, proizvod.Naziv)
                    .SetProperty(x => x.Cena, proizvod.Cena)
                    .SetProperty(x => x.Kolicina, proizvod.Kolicina)
                    .SetProperty(x => x.Opis, proizvod.Opis)
                    .SetProperty(x => x.KategorijaId, proizvod.KategorijaId)
                );

            return true;
        }

        public bool ObrisiProizvod(int ProizvodID)
        {
            var proizovd = _context.Proizvods.FirstOrDefault(p => p.ProizvodId == ProizvodID);
            var provera = _context.Narudzbinas.FirstOrDefault(n => n.ProizvodId == ProizvodID);

            if (proizovd != null)
            {

                if (provera == null)
                {
                    _context.Proizvods.Remove(proizovd);

                    if (_context.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;


            }
            else
            {
                return false;
            }
        }

        public bool DodajProizovd(DodajProizvod noviProizvod)
        {


            var dodajProizovd = new Proizvod()
            {
                Naziv = noviProizvod.Naziv,
                Cena = noviProizvod.Cena,
                Kolicina = noviProizvod.Kolicina,
                Opis = noviProizvod.Opis,
                KorisnikId = noviProizvod.KorisnikId,
                KategorijaId = noviProizvod.KategorijaId
            };

            if (dodajProizovd != null)
            {
                _context.Proizvods.Add(dodajProizovd);

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
    }
}
