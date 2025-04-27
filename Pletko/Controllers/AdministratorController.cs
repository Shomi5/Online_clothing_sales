using Microsoft.AspNetCore.Mvc;
using Pletko.Models.EFRepository;
using Pletko.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Pletko.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly PletkoContext _context;
        private AdminRepository repos;


        public AdministratorController()
        {
            _context = new PletkoContext();
            repos = new AdminRepository();
        }

        [Authorize]
        public IActionResult AdminPanel()
        {


            string? email = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");
            ViewBag.Logovan = email != null;
            ViewBag.Rola = rola;

            if (email != null)
            {
                if(rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            ViewBag.Narudzbine = repos.SveNarudzbe();
            ViewBag.Korisnici = repos.SviKorisnici();
            ViewBag.Proizvodi = repos.SviProizvodi();

            if (TempData["Azuriraj"] != " " && TempData["Azuriraj"] != null)
            {
                ViewBag.Azuriraj = TempData["Azuriraj"];
            }

            if (TempData["OtkazanaNarudzba"] != " " && TempData["OtkazanaNarudzba"] != null)
            {
                ViewBag.OtkazanaNarudzbina = TempData["OtkazanaNarudzba"];
            }
            

            if (TempData["DodatKorisnik"] != " " && TempData["DodatKorisnik"] != null)
            {
                ViewBag.DodatKorisnik = TempData["DodatKorisnik"];
            }
            
            if (TempData["ObrisanKorisnik"] != " " && TempData["ObrisanKorisnik"] != null)
            {
                ViewBag.ObrisanKorisnik = TempData["ObrisanKorisnik"];
            }

            if (TempData["Proizvod"] != " " && TempData["Proizvod"] != null)
            {
                ViewBag.Proizvod = TempData["Proizvod"];
            }

            if (TempData["DodajProizvod"] != " " && TempData["DodajProizvod"] != null)
            {
                ViewBag.DodajProizvod = TempData["DodajProizvod"];
            }


            return View();
        }

        [Authorize]
        public IActionResult EditPodatakaPartial(int KorisnikID)
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }


            ViewBag.TrazeniKorisnik = repos.NadjiKorisnika(KorisnikID);
            return PartialView("_EditPodatakaPartial");
        }

        [HttpPost]
        [Authorize]
        public IActionResult azurirajPodatke(AzurirajPodatke korisnik)
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            if (korisnik != null)
            {
                ViewBag.Narudzbine = repos.SveNarudzbe();
                ViewBag.Korisnici = repos.SviKorisnici();
                ViewBag.Proizvodi = repos.SviProizvodi();
                
                ViewBag.TrazeniKorisnik = repos.NadjiKorisnika(korisnik.KorisnikId);


                if (ModelState.IsValid)
                {
                    string? email = HttpContext.Session.GetString("Email");

                    string? porediSifre = _context.Korisniks.Where(x => x.Email == email).Select(x => x.Password).SingleOrDefault();

                    if (porediSifre == korisnik.OldPassword)
                    {
                        if (repos.AzurirajPodatkeAdmin(korisnik))
                        {
                            return Json(new { success = true, message = "Uspesno ste ažurirali podatke!" , korisnikId = korisnik.KorisnikId});
                        }
                        else
                        {
                            return Json(new { success = false, message = "Došlo je do greške usled ažuriranja podataka!" });
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "Morate unet lozinku administratora.");
                    }

                }

                return View("_EditPodatakaPartial");
            }

            return Json(new { success = false, message = "Došlo je do greške!" });
        }


        [Authorize]
        public IActionResult DodajKorisnika()
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            ViewBag.Statusi = repos.SviStatusi();
            return PartialView("_dodavanjeKorisnika");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DodajKorisnika(AddUser korisnik)
        {

            var email = _context.Korisniks.Where(e => e.Email == korisnik.Email).FirstOrDefault();

            string? emailAdmina = HttpContext.Session.GetString("Email");

            string? porediSifre = _context.Korisniks.Where(x => x.Email == emailAdmina).Select(x => x.Password).SingleOrDefault();

            if (email == null)
            {

                if(porediSifre == korisnik.OldPassword)
                {
                    if (ModelState.IsValid)
                    {

                        if (repos.DodajKorisnika(korisnik))
                        {
                            TempData["DodatKorisnik"] = "Uspešno ste dodali korisnika.";
                            return Json(new { success = true });
                            //return RedirectToAction("AdminPanel");
                        }
                        else
                        {
                            TempData["DodatKorisnik"] = "Korisnik nije dodat!";
                            return Json(new { success = false});
                            //return RedirectToAction("AdminPanel");
                        }

                    }
                    else
                    {
                        ViewBag.Statusi = repos.SviStatusi();
                        return View("_dodavanjeKorisnika");
                    }
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Morate unet lozinku administratora.");
                    ViewBag.Statusi = repos.SviStatusi();
                    return View("_dodavanjeKorisnika");
                }
               
            }
            else
            {
                ModelState.AddModelError("Email", "E-mail adresa je već u upotrebi.");
                ViewBag.Statusi = repos.SviStatusi();
                return View("_dodavanjeKorisnika");
            }

        }

        [Authorize]
        public IActionResult OtkaziKorisnickuNarudzbu(int NarudzbinaID)
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            if (NarudzbinaID > 0 && NarudzbinaID != null)
            {

                if (repos.OtkaziKorisnickuNarudzbinu(NarudzbinaID))
                {
                    TempData["OtkazanaNarudzba"] = "Narudžbina je uspešno otkazana!";
                    return RedirectToAction("AdminPanel");
                }
                else
                {
                    TempData["OtkazanaNarudzba"] = "Narudzbenica nije pronadjena!";
                    return RedirectToAction("AdminPanel");
                }


            }
            else
            {
                TempData["OtkazanaNarudzba"] = "Niste odabrali narudžbinu!";
                return RedirectToAction("AdminPanel");
            }

        }

        [Authorize]
        public IActionResult ObrisiKorisnika(int KorisnikID)
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }


            if (KorisnikID > 0 && KorisnikID != null)
            {
                if (repos.ObrisiKorinika(KorisnikID)) 
                {
                    TempData["ObrisanKorisnik"] = "Korisnik je uspešno obrisan!";
                    return RedirectToAction("AdminPanel");
                }
                else
                {
                    TempData["ObrisanKorisnik"] = "Nije moguće obrisati korisnika koji ima narudžbinu!";
                    return RedirectToAction("AdminPanel");
                }

            }

            return Json(new { success = false, message = "Došlo je do greške!" });
        }

        [Authorize]
        public IActionResult EditProizvoda(int ProizvodID)
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            ViewBag.TrazeniProizvod = repos.NadjiProizvod(ProizvodID);
            ViewBag.SveKategorije = repos.SveKategorije();
            return PartialView("_EditProizvoda");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AzurirajPodatkeProizvoda(ProizovdEdit proizvod)
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            if (proizvod != null)
            {
                ViewBag.Narudzbine = repos.SveNarudzbe();
                ViewBag.Korisnici = repos.SviKorisnici();
                ViewBag.Proizvodi = repos.SviProizvodi();

                ViewBag.TrazeniProizvod = repos.NadjiProizvod(proizvod.ProizvodId);
                ViewBag.SveKategorije = repos.SveKategorije();

                if (proizvod.SlikaArtikla != null && proizvod.SlikaArtikla.Length > 0)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "proizvodImages");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var fileName = proizvod.Naziv + Path.GetExtension(proizvod.SlikaArtikla.FileName); // Generisanje nasumičnog imena
                    var filePath = Path.Combine(folderPath, fileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }


                    var stream = new FileStream(filePath, FileMode.Create);
                    proizvod.SlikaArtikla.CopyTo(stream);
                }

                if (ModelState.IsValid)
                {
                    string? email = HttpContext.Session.GetString("Email");

                    string? porediSifre = _context.Korisniks.Where(x => x.Email == email).Select(x => x.Password).SingleOrDefault();

                    if (porediSifre == proizvod.OldPassword)
                    {
                        if (repos.AzurirajPodatkeProizvoda(proizvod))
                        {

                            return Json(new { success = true, message = "Uspesno ste ažurirali podatke!", proizvodId = proizvod.ProizvodId });
                        }
                        else
                        {
                            return Json(new { success = false, message = "Došlo je do greške usled ažuriranja podataka!" });
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "Morate unet lozinku administratora.");
                    }

                }


                return View("_EditProizvoda");
            }

            return Json(new { success = false, message = "Došlo je do greške!" });
        }

        [Authorize]
        public IActionResult DodajProizvod()
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            string? emailAdmina = HttpContext.Session.GetString("Email");

            int administrator = _context.Korisniks.Where(x => x.Email == emailAdmina).Select(x=> x.KorisnikId).SingleOrDefault();
            ViewBag.Admin = administrator;
            ViewBag.Kategorije = repos.SveKategorije();
            return PartialView("_dodajProizvod");
        }

        [Authorize]
        [HttpPost]

        public IActionResult DodajNoviProizvod(DodajProizvod noviProizovd)
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            string? emailAdmina = HttpContext.Session.GetString("Email");

            Korisnik? porediSifre = _context.Korisniks.Where(x => x.Email == emailAdmina).SingleOrDefault();

            if (porediSifre.Password == noviProizovd.OldPassword)
            {
                if (noviProizovd.SlikaArtikla != null && noviProizovd.SlikaArtikla.Length > 0)
                {

                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", "proizvodImages");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    //var fileName = noviProizovd.Naziv + Path.GetExtension(noviProizovd.SlikaArtikla.FileName); // Generisanje nasumičnog imena
                    //var filePath = Path.Combine(folderPath, fileName);

                    //if (System.IO.File.Exists(filePath))
                    //{
                    //    System.IO.File.Delete(filePath);
                    //}


                    //var stream = new FileStream(filePath, FileMode.Create);
                    //noviProizovd.SlikaArtikla.CopyTo(stream);

                    var fileName = noviProizovd.Naziv + Path.GetExtension(noviProizovd.SlikaArtikla.FileName); // Generisanje naziva fajla
                    var filePath = Path.Combine(folderPath, fileName);

                    // Ako fajl postoji, pokušajte da ga obrišete
                    if (System.IO.File.Exists(filePath))
                    {
                        try
                        {
                            // Otvorite fajl sa FileShare.Delete da omogućite njegovo brisanje
                            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete))
                            {
                                // Obrišite fajl
                                System.IO.File.Delete(filePath);
                            }
                        }
                        catch (IOException ex)
                        {
                            // Obrada greške u slučaju da je fajl zaključan
                            Console.WriteLine($"Greška pri pokušaju brisanja fajla: {ex.Message}");
                        }
                    }

                    // Kreirajte novi FileStream za snimanje fajla
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        // Kopirajte podatke u novi fajl
                        noviProizovd.SlikaArtikla.CopyTo(stream);
                    }


                    if (ModelState.IsValid)
                    {



                        if (repos.DodajProizovd(noviProizovd))
                        {
                            TempData["DodajProizvod"] = "Uspešno ste dodali Proizvod.";
                            return Json(new { success = true });
                            //return RedirectToAction("AdminPanel");
                        }
                        else
                        {
                            TempData["DodajProizvod"] = "Proizovd nije dodat!";
                            return Json(new { success = false });
                            //return RedirectToAction("AdminPanel");
                        }

                    }
                    else
                    {
                        return View("_dodajProizvod");
                    }
                }
                else
                {
                    ModelState.AddModelError("novaSlika", "Morate izabrati sliku proizvoda.");
                    ViewBag.Admin = porediSifre.KorisnikId;
                    ViewBag.Kategorije = repos.SveKategorije();
                    return View("_dodajProizvod");
                }
            }
            else
            {
                ModelState.AddModelError("OldPassword", "Morate unet lozinku administratora.");
                ViewBag.Admin = porediSifre.KorisnikId;
                ViewBag.Kategorije = repos.SveKategorije();
                return View("_dodajProizvod");
            }

           
        }

        [Authorize]
        public IActionResult ObrisiProizvod(int ProizovdID)
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            if (ProizovdID > 0 && ProizovdID != null)
            {
                if (repos.ObrisiProizvod(ProizovdID))
                {
                    TempData["Proizvod"] = "Proizvod je uspešno obrisan!";
                    return RedirectToAction("AdminPanel");
                }
                else
                {
                    TempData["Proizvod"] = "Nije moguce izbrisati rezervisan proizvod!";
                    return RedirectToAction("AdminPanel");
                }

            }

            return Json(new { success = false, message = "Došlo je do greške!" });
        }
    }
}

