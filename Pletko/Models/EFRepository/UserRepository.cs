using Microsoft.EntityFrameworkCore;
using Pletko.Models.Interface;

namespace Pletko.Models.EFRepository
{
    public class UserRepository:IUserRepository
    {
        private readonly PletkoContext _context = new PletkoContext();

        public void RegistrujKorisnika(UserRegister user)
        {
            var korisnik = new Korisnik
            {
                Ime = user.Ime,
                Prezime = user.Prezime,
                Email = user.Email,
                Adresa = user.Adresa,
                DatumRodjenja = user.DatumRodjenja,
                Password = user.Password,
                BrojTelefona = user.BrojTelefona,
            };

            _context.Korisniks.Add(korisnik);

            _context.SaveChangesAsync();
        }


        public IEnumerable<ProizvodBO> ListaSvihProizvoda()
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

        public IEnumerable<KategorijaBO> SveKategorije()
        {
            List<KategorijaBO> Svekategorije = new List<KategorijaBO>();

            foreach(Kategorija kategorije in _context.Kategorijas.ToList())
            {
                KategorijaBO kate = new KategorijaBO()
                {
                    KategorijaId = kategorije.KategorijaId,
                    Naziv = kategorije.Naziv,
                };

                Svekategorije.Add(kate);
            }

            return Svekategorije;
        }

        public IEnumerable<ProizvodBO> PretraziKategoriju(string KategorijaNaziv)
        {
            List<ProizvodBO> KateProizvoda = new List<ProizvodBO>();

            foreach(Proizvod proizvod in _context.Proizvods.Where(p => p.Kategorija.Naziv.Contains(KategorijaNaziv)).ToList())
            {
                Kategorija? kategorija = _context.Kategorijas.SingleOrDefault(k => k.Naziv == KategorijaNaziv);
                Proizvod? trazeniProizvod = _context.Proizvods.Where(s=>s.ProizvodId == proizvod.ProizvodId).SingleOrDefault(x=> x.KategorijaId == kategorija.KategorijaId);
                

                ProizvodBO nadjeProizvod = new ProizvodBO()
                {
                    ProizvodId = trazeniProizvod.ProizvodId,
                    Naziv = trazeniProizvod.Naziv,
                    Cena = trazeniProizvod.Cena,
                    Kolicina = trazeniProizvod.Kolicina,
                    Opis = trazeniProizvod.Opis,
                    KorisnikId = trazeniProizvod?.KorisnikId,
                    Kategorija = new KategorijaBO()
                    {
                        KategorijaId = kategorija.KategorijaId,
                        Naziv = kategorija.Naziv,
                    }

                };

                KateProizvoda.Add(nadjeProizvod);
            }

            return KateProizvoda;
        }

        public ProizvodBO Narudzbina(int ProizvodID)
        {
            Proizvod? proizvod = _context.Proizvods.SingleOrDefault(p => p.ProizvodId == ProizvodID);
            ProizvodBO trazeniPoizvod = new ProizvodBO()
            {
                ProizvodId = proizvod.ProizvodId,
                Naziv = proizvod.Naziv,
                Cena = proizvod.Cena,
                Kolicina = proizvod.Kolicina,
                Opis = proizvod.Opis,
                KorisnikId = proizvod.KorisnikId
            };

            return trazeniPoizvod;

        }


        public void NarudzbaProizvoda(KorisnikNarucivanje narudzba)
        {

            Korisnik? korisnik = _context.Korisniks.SingleOrDefault(k=>k.Email == narudzba.Email);
            Proizvod? proizvod = _context.Proizvods.SingleOrDefault(p => p.ProizvodId == narudzba.ProizvodID);


            if(narudzba.Popust > 0)
            {
                var novaNarudzbina = new Narudzbina()
                {
                    BrojKartice = narudzba.BrojKartice,
                    KorisnikId = korisnik.KorisnikId,
                    ProizvodId = proizvod.ProizvodId,
                    Kolicina = narudzba.Kolicina,
                    UkupnaCena = (proizvod.Cena / (narudzba.Popust / 100 + 1)) * narudzba.Kolicina,
                };

                _context.Korisniks
                .Where(x => x.KorisnikId == korisnik.KorisnikId)
                .ExecuteUpdate(setters => setters
                    .SetProperty(x => x.PonudaId, (int?)null)
                );



                _context.Narudzbinas.Add(novaNarudzbina);

                _context.SaveChangesAsync();
            }
            else
            {
                var novaNarudzbina = new Narudzbina()
                {
                    BrojKartice = narudzba.BrojKartice,
                    KorisnikId = korisnik.KorisnikId,
                    ProizvodId = proizvod.ProizvodId,
                    Kolicina = narudzba.Kolicina,
                    UkupnaCena = proizvod.Cena * narudzba.Kolicina,
                };

                _context.Narudzbinas.Add(novaNarudzbina);

                _context.SaveChangesAsync();
            }

            
        }

        /* Profil korisnika */

        public IEnumerable<NarudzbinaBO> TrazeneNarudzbine(int KorisnikID)
        {
            List<NarudzbinaBO> narudzbine = new List<NarudzbinaBO>();

            foreach (Narudzbina narudzbinaSve in _context.Narudzbinas.Where(x => x.KorisnikId == KorisnikID).ToList())
            {
                Korisnik? podaci = _context.Korisniks.SingleOrDefault(k => k.KorisnikId == KorisnikID);
                Proizvod proizvod = _context.Proizvods.SingleOrDefault(p => p.ProizvodId == narudzbinaSve.ProizvodId);

                var sveNarudzbe = new NarudzbinaBO()
                {
                    NarudzbinaId = narudzbinaSve.NarudzbinaId,
                    DatumNarudzbine = narudzbinaSve.DatumNarudzbine,
                    BrojKartice = narudzbinaSve.BrojKartice,
                    KorisnikId = podaci.KorisnikId,
                    ProizvodId = proizvod.ProizvodId,
                    Kolicina = narudzbinaSve.Kolicina,
                    UkupnaCena = narudzbinaSve.UkupnaCena,
                    Korisnik = new KorisnikBO()
                    {
                        KorisnikId = podaci.KorisnikId,
                        Ime = podaci.Ime,
                        Prezime = podaci.Prezime,
                        Email = podaci.Email,
                        Adresa = podaci.Adresa,
                        DatumRodjenja = podaci.DatumRodjenja,
                        Password = podaci.Password,
                        BrojTelefona = podaci.BrojTelefona,

                    },
                    Proizvod = new ProizvodBO()
                    {
                        ProizvodId = proizvod.ProizvodId,
                        Naziv = proizvod.Naziv,
                        Cena = proizvod.Cena,
                        Kolicina = proizvod.Kolicina,
                        Opis = proizvod.Opis
                    }

                };

                narudzbine.Add(sveNarudzbe);
            }

            return narudzbine;
        }

        public bool OtkaziNarudzbinu(int NarudzbinaID)
        {
            bool obrisano = false;

            var narudzbina = _context.Narudzbinas.FirstOrDefault(n => n.NarudzbinaId == NarudzbinaID);

            if (narudzbina != null)
            {
                _context.Narudzbinas.Remove(narudzbina);
                _context.SaveChanges();

                obrisano = true;
            }
            else
            {
                obrisano = false;
            }

            return obrisano;
        }

        public bool AzurirajPodatke(AzurirajPodatke Korisnik)
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
    }
}
