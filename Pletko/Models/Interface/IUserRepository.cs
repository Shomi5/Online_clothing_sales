namespace Pletko.Models.Interface
{
    public interface IUserRepository
    {
        void RegistrujKorisnika(UserRegister user);

        IEnumerable<ProizvodBO> ListaSvihProizvoda();

        IEnumerable<KategorijaBO> SveKategorije();

        IEnumerable<ProizvodBO> PretraziKategoriju(string KategorijaNaziv);

        ProizvodBO Narudzbina(int ProizvodID);
        void NarudzbaProizvoda(KorisnikNarucivanje narudzba);

        IEnumerable<NarudzbinaBO> TrazeneNarudzbine(int KorisnikID);

        bool OtkaziNarudzbinu(int NarudzbinaID);
        bool AzurirajPodatke(AzurirajPodatke Korisnik);
    }
}
