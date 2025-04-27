using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pletko.Models;
using Pletko.Models.EFRepository;

namespace Pletko.Controllers
{
    public class ModeratorController : Controller
    {
        private readonly PletkoContext _context;
        ModeratorRepository repos;
        AdminRepository arepos;


        public ModeratorController()
        {
            repos = new ModeratorRepository();
            _context = new PletkoContext();
            arepos = new AdminRepository();
        }

        [Authorize]
        public IActionResult ModeratorPanel()
        {

            string? email = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");
            ViewBag.Logovan = email != null;
            ViewBag.Rola = rola;

            if (email != null)
            {
                if (rola != "Admin" && rola != "Moderator")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
            
            if (TempData["DodajPonudu"] != null && TempData["DodajPonudu"] != "")
            {
                ViewBag.DodajPonudu = TempData["DodajPonudu"];
            }
            
            if (TempData["Ponuda"] != null && TempData["Ponuda"] != "")
            {
                ViewBag.Ponuda = TempData["Ponuda"];
            }
            
            if (TempData["UkloniPonudu"] != null && TempData["UkloniPonudu"] != "")
            {
                ViewBag.UkloniPonudu = TempData["UkloniPonudu"];
            }

            if (TempData["DodajPonuduKorisniku"] != null && TempData["DodajPonuduKorisniku"] != "")
            {
                ViewBag.DodajPonuduKorisniku = TempData["DodajPonuduKorisniku"];
            }

            ViewBag.Ponude = repos.SvePonude();
            ViewBag.Korisnici = repos.SviKorisnici();
            return View();
        }

        [Authorize]
        public IActionResult DodajPonudu()
        {
            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin" && rola != "Moderator")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            int radnik = _context.Korisniks.Where(x => x.Email == Proveraemail).Select(x => x.KorisnikId).SingleOrDefault();
            ViewBag.Radnik = radnik;

            return PartialView("_ponudaDodaj");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DodajNovuPonudu(SpecijalnaPonudaBO novaPonuda)
        {
            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin" && rola != "Moderator")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            Korisnik? porediSifre = _context.Korisniks.Where(x => x.Email == Proveraemail).SingleOrDefault();

            if(porediSifre.Password == novaPonuda.OldPassword)
            {
                if (ModelState.IsValid)
                {
                    if (repos.DodajPonudu(novaPonuda))
                    {
                        TempData["DodajPonudu"] = "Uspešno ste dodali ponudu.";
                        return Json(new { success = true });
                        //return RedirectToAction("AdminPanel");
                    }
                    else
                    {
                        TempData["DodajPonudu"] = "Ponuda nije dodat!";
                        return Json(new { success = false });
                        //return RedirectToAction("AdminPanel");
                    }
                }
                else
                {
                    ViewBag.Radnik = porediSifre.KorisnikId;
                    return View("_ponudaDodaj");
                }
            }
            else
            {
                ModelState.AddModelError("OldPassword", "Morate unet lozinku administratora.");
                ViewBag.Radnik = porediSifre.KorisnikId;
                return View("_ponudaDodaj");
            }
        }

        [Authorize]
        public IActionResult EditPonuda(int PonudaID)
        {
            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin" && rola != "Moderator")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            Korisnik? porediSifre = _context.Korisniks.Where(x => x.Email == Proveraemail).SingleOrDefault();

            ViewBag.trazenaPonuda = repos.nadjiPonudu(PonudaID);
            ViewBag.Radnik = porediSifre.KorisnikId;
            return PartialView("_PonudaEdit");
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditPonude(SpecijalnaPonudaBO editPonuda) {


            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin" && rola != "Moderator")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            if (editPonuda != null)
            {
               
                if (ModelState.IsValid)
                {
                    string? email = HttpContext.Session.GetString("Email");

                    string? porediSifre = _context.Korisniks.Where(x => x.Email == email).Select(x => x.Password).SingleOrDefault();

                    if (porediSifre == editPonuda.OldPassword)
                    {
                        if (repos.EditPonude(editPonuda))
                        {

                            return Json(new { success = true, message = "Uspesno ste ažurirali podatke!", ponudaId = editPonuda.PonudaId });
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

                return View("_PonudaEdit");
            }

            return Json(new { success = false, message = "Došlo je do greške!" });
        }

        [Authorize]
        public IActionResult obrisiPonudu(int PonudaID)
        {
            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin" && rola != "Moderator")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            if (PonudaID > 0 && PonudaID != null)
            {
                if (repos.ObrisiPonudu(PonudaID))
                {
                    TempData["Ponuda"] = "Ponuda je uspešno obrisan!";
                    return RedirectToAction("ModeratorPanel");
                }
                else
                {
                    TempData["Ponuda"] = "Došlo je do greške prilikom brisanja ponude!";
                    return RedirectToAction("ModeratorPanel");
                }

            }

            return Json(new { success = false, message = "Došlo je do greške!" });
        }

        [Authorize]
        public IActionResult PonudaKorisnik(int KorisnikID)
        {

            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin" && rola != "Moderator")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            ViewBag.Ponude = repos.SvePonude();
            ViewBag.Korisnik = arepos.NadjiKorisnika(KorisnikID);
            return PartialView("_korisikPonuda");
        }

        [HttpPost]
        public IActionResult DodeliPonuduKorisniku(SPKorisnika ponudaKorisniku)
        {
            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin" && rola != "Moderator")
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            Korisnik? porediSifre = _context.Korisniks.Where(x => x.Email == Proveraemail).SingleOrDefault();

            if (porediSifre.Password == ponudaKorisniku.OldPassword)
            {

                if (ponudaKorisniku.PonudaId > 0 && ponudaKorisniku.PonudaId != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (repos.DodeliPonudu(ponudaKorisniku))
                        {
                            TempData["DodajPonuduKorisniku"] = "Uspešno ste dodelili ponudu korisniku!";
                            return Json(new { success = true });
                            //return RedirectToAction("AdminPanel");
                        }
                        else
                        {
                            TempData["DodajPonuduKorisniku"] = "Ponuda nije dodeljena!";
                            return Json(new { success = false });
                            //return RedirectToAction("AdminPanel");
                        }
                    }
                    else
                    {
                        ViewBag.Radnik = porediSifre.KorisnikId;
                        return View("_korisikPonuda");
                    }
                }
                else
                {
                    ModelState.AddModelError("PonudaId", "Morate izabrati ponudu za korisnika.");
                    ViewBag.Ponude = repos.SvePonude();
                    ViewBag.Korisnik = arepos.NadjiKorisnika(ponudaKorisniku.KorisnikId);
                    return View("_korisikPonuda");
                }
                
            }
            else
            {
                ModelState.AddModelError("OldPassword", "Morate unet lozinku administratora.");
                ViewBag.Ponude = repos.SvePonude();
                ViewBag.Korisnik = arepos.NadjiKorisnika(ponudaKorisniku.KorisnikId);
                return View("_korisikPonuda");
            }

        }

        public IActionResult OtkaziKorisnickuPonudu(int KorisnikID)
        {
            string? Proveraemail = HttpContext.Session.GetString("Email");
            string? rola = HttpContext.Session.GetString("Rola");

            if (Proveraemail != null)
            {
                if (rola != "Admin" && rola != "Moderator")
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
                if (repos.OtkaziPonudu(KorisnikID))
                {
                    TempData["UkloniPonudu"] = "Uspesno ste uklonili ponudu korisnika!";
                    return RedirectToAction("ModeratorPanel");
                }
                else
                {
                    TempData["UkloniPonudu"] = "Došlo je do greške prilikom brisanja ponude korisnika!";
                    return RedirectToAction("ModeratorPanel");
                }

            }

            return Json(new { success = false, message = "Došlo je do greške!" });
        }
    }
}
