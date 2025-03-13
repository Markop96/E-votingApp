using E_glasanje.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_glasanje.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Ime,Prezime,DatumRodjenja,Telefon,JMBG")] User user)
        {
            if (ModelState.IsValid)
            {
                // Proverite da li već postoji korisnik sa istim JMBG
                var existingUser = _context.Users.FirstOrDefault(u => u.JMBG == user.JMBG);
                if (existingUser != null)
                {
                    // Ako korisnik sa tim JMBG već postoji, preusmerite na stranicu "Registration Denied"
                    return RedirectToAction("RegistrationDenied");
                }

                // Ako ne postoji, dodajte korisnika u bazu
                _context.Users.Add(user);
                _context.SaveChanges();

                // Dohvatite korisnika iz baze prema JMBG-u
                var userFromDb = _context.Users.FirstOrDefault(u => u.JMBG == user.JMBG);
                if (userFromDb != null)
                {
                    // Postavite vrednosti u ViewBag
                    ViewBag.IsSuccess = true;
                    ViewBag.JMBG = userFromDb.JMBG;
                    ViewBag.Lozinka = userFromDb.Sifra; // Pretpostavljam da je "Sifra" lozinka korisnika
                }

                // Preusmerite na stranicu za uspešnu registraciju
                return View("RegistrationSuccess");
            }

            return View(user);
        }

        // Prikaz stranice za uspešnu registraciju
        public IActionResult RegistrationSuccess()
        {
            return View();
        }

        // Prikaz stranice za neuspešnu registraciju
        public IActionResult RegistrationDenied()
        {
            ViewBag.Message = "Korisnik sa tim JMBG već postoji u sistemu.";
            return View();
        }
    }
}
