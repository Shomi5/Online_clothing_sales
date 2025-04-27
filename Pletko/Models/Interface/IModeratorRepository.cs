namespace Pletko.Models.Interface
{
    public interface IModeratorRepository
    {
        IEnumerable<KorisnikBO> SviKorisnici();
        IEnumerable<SpecijalnaPonudaBO> SvePonude();

        bool DodajPonudu(SpecijalnaPonudaBO novaPonuda);
        SpecijalnaPonudaBO nadjiPonudu(int ponudaId);

        bool EditPonude(SpecijalnaPonudaBO editPonuda);
        bool ObrisiPonudu(int PonudaID);

        bool DodeliPonudu(SPKorisnika ponudaKorisnika);

        bool OtkaziPonudu(int KorisnikID);
    }
}
