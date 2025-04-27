namespace Pletko.Models.Interface
{
    public interface IAdminRepository
    {
        IEnumerable<KorisnikBO> SviKorisnici();
        IEnumerable<ProizvodBO> SviProizvodi();
        IEnumerable<NarudzbinaBO> SveNarudzbe();
        IEnumerable<UserStatusBO> SviStatusi();
        IEnumerable<KategorijaBO> SveKategorije();

        KorisnikBO NadjiKorisnika(int KorisnikID);
        bool OtkaziKorisnickuNarudzbinu(int NarudzbinaID);

        bool DodajKorisnika(AddUser korisnik);

        bool AzurirajPodatkeAdmin(AzurirajPodatke Korisnik);

        bool ObrisiKorinika(int KorisnikID);

        ProizvodBO NadjiProizvod(int ProizvodId);

        bool DodajProizovd(DodajProizvod noviProizvod);
        bool AzurirajPodatkeProizvoda(ProizovdEdit proizvod);

        bool ObrisiProizvod(int ProizvodID);
    }
}
