using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pletko.Models;
using Pletko.Models.EFRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Authorization;

namespace Pletko.Controllers
{

    public class UserController : Controller
    {
        private readonly PletkoContext _context;
        private UserRepository repos;
        private AdminRepository arepos;

        public UserController()
        {
            _context = new PletkoContext();
            repos= new UserRepository();
            arepos = new AdminRepository();


        }

        [AllowAnonymous]
        public IActionResult Index()
        {

            ViewBag.Proizvodi = repos.ListaSvihProizvoda();
            ViewBag.Kategorije = repos.SveKategorije();
            string? email = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");
            ViewBag.Logovan = email != null;
            ViewBag.Rola = rola;
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(UserRegister user)
        {
            var email = _context.Korisniks.Where(e=>e.Email == user.Email).FirstOrDefault();

            if (email == null)
            {
                if(ModelState.IsValid)
                {

                    repos.RegistrujKorisnika(user);
                    return RedirectToAction("Login");
                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("Email", "E-mail adresa je već u upotrebi.");
                return View(user);
            }
            
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                return RedirectToAction("Index"); 
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin user)
        {

            if (ModelState.IsValid)
            {

                var userLog = await _context.Korisniks.Where(x => x.Email == user.Email).SingleOrDefaultAsync();
                var rola = await _context.UserStatuses.Where(s => s.StatusId == userLog.StatusId).SingleOrDefaultAsync();

                if (userLog != null && userLog.Password == user.Password && rola != null)
                {
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Rola", rola.Naziv);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Email", "Uneti podaci ne pripadaju ni jednom korisniku.");
                    return View(user);
                }

            }
            else
            {
                return View(user);
            }
        }
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Rola");

            return RedirectToAction("Index");
        }

        public IActionResult Pretraga(string KategorijaNaziv)
        {
            ViewBag.KategorijaArtikala = repos.PretraziKategoriju(KategorijaNaziv);
            ViewBag.Kategorije = repos.SveKategorije();
            string? email = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");
            ViewBag.Logovan = email != null;
            ViewBag.Rola = rola;

            return View("Pretraga");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Narudzbina(int ProizvodID)
        {

            if (ProizvodID != null && ProizvodID > 0)
            {
                string? email = HttpContext.Session.GetString("Email");
                ViewBag.Logovan = email != null; //Korisit za provere
                if (email != null)
                {
                    var korisnik = _context.Korisniks.Where(x => x.Email == email).SingleOrDefault();
                    ViewBag.Korisnik = arepos.NadjiKorisnika(korisnik.KorisnikId); //Koristi za ispis
                }

                if (TempData["SuccessMessage"] != " " && TempData["SuccessMessage"] != null)
                {
                    ViewBag.SuccessMessage = TempData["SuccessMessage"];
                }
                ViewBag.TrazeniProizvod = repos.Narudzbina(ProizvodID);
                return View("Narudzbina");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Narudzbinad(KorisnikNarucivanje narudzba)
        {

            if (ModelState.IsValid)
            {

                var proveraEmail = _context.Korisniks.Where(x => x.Email == narudzba.Email).SingleOrDefaultAsync();

                if (proveraEmail != null)
                {
                    repos.NarudzbaProizvoda(narudzba);
                    //return RedirectToAction("Index");

                    TempData["SuccessMessage"] = "Narudžbina je uspešno izvršena!";
                    ViewBag.test = narudzba.ProizvodID;
                    return RedirectToAction("Narudzbina", new { ProizvodID = narudzba.ProizvodID });
                }
                else
                {
                    ModelState.AddModelError("Email", "Ovaj e-mail nije validan.");
                    return View("Narudzbina", narudzba.ProizvodID);
                }

            }
            else
            {
                return View("Narudzbina", narudzba.ProizvodID);
            }
        }

        /* User Profil*/
        [HttpGet]
        [Authorize]
        public IActionResult userProfil()
        {
            string? email = HttpContext.Session.GetString("Email");
            ViewBag.Logovan = email != null; 
            if (email != null)
            {
                int pronadjiID = _context.Korisniks.Where(x => x.Email == email).Select(x => x.KorisnikId).SingleOrDefault();
                ViewBag.PodaciNarudzbina = repos.TrazeneNarudzbine(pronadjiID);
                ViewBag.KorisnickiPodatci = _context.Korisniks.Where(x => x.Email == email).SingleOrDefault();

                if (TempData["OtkazanaNarudzba"] != " " && TempData["OtkazanaNarudzba"] != null)
                {
                    ViewBag.OtkazanaNarudzbina = TempData["OtkazanaNarudzba"];
                }

                if (TempData["Azuriraj"] != " " && TempData["Azuriraj"] != null)
                {
                    ViewBag.Azuriraj = TempData["Azuriraj"];
                }


                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }


        }

        [HttpPost]
        [Authorize]
        public IActionResult userProfil(AzurirajPodatke korisnik)
        {
            if (korisnik != null)
            {
                string? email = HttpContext.Session.GetString("Email");
                int pronadjiID = _context.Korisniks.Where(x => x.Email == email).Select(x => x.KorisnikId).SingleOrDefault();
                ViewBag.PodaciNarudzbina = repos.TrazeneNarudzbine(pronadjiID);
                ViewBag.KorisnickiPodatci = _context.Korisniks.Where(x => x.Email == email).SingleOrDefault();


                if (ModelState.IsValid)
                {

                    string? porediSifre = _context.Korisniks.Where(x => x.Email == email).Select(x => x.Password).SingleOrDefault();

                    if (porediSifre == korisnik.OldPassword)
                    {
                        if (repos.AzurirajPodatke(korisnik))
                        {
                            TempData["Azuriraj"] = "Uspesno ste ažurirali podatke!";
                            return RedirectToAction("userProfil");
                        }
                        else
                        {
                            TempData["Azuriraj"] = "Došlo je do greške usled ažuriranja podataka!";
                            return RedirectToAction("userProfil");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "Morate uneti staru šifru.");
                    }

                }

                return View("userProfil");
            }

            return View("userProfil");
        }

        [HttpGet]
        public IActionResult Odustani(int NarudzbinaID)
        {

            if(NarudzbinaID > 0 && NarudzbinaID != null)
            {

                if (repos.OtkaziNarudzbinu(NarudzbinaID))
                {
                    TempData["OtkazanaNarudzba"] = "Narudžbina je uspešno otkazana!";
                    return RedirectToAction("userProfil");
                }
                else
                {
                    TempData["OtkazanaNarudzba"] = "Narudzbenica nije pronadjena!";
                    return RedirectToAction("userProfil");
                }

                
            }
            else
            {
                TempData["OtkazanaNarudzba"] = "Niste odabrali narudžbinu!";
                return RedirectToAction("userProfil");
            }

            
        }
    }
}
