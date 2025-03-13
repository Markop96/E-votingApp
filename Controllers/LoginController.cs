using E_glasanje.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_glasanje.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("JMBG,Sifra")] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Provera korisnika sa JMBG i Sifra
                var user = _context.Users.FirstOrDefault(u => u.JMBG == loginModel.JMBG && u.Sifra == loginModel.Sifra);

                if (user != null)
                {
                    // Sačuvaj JMBG korisnika u sesiji
                    HttpContext.Session.SetString("UserJMBG", loginModel.JMBG);

                    // Preusmeravanje na stranicu za glasanje
                    return RedirectToAction("Create", "Vote");
                }
                else
                {
                    ModelState.AddModelError("", "Pogrešan JMBG ili lozinka");
                }
            }

            return View("Index", loginModel);
        }


    }
}
