using E_glasanje.Models;
using Microsoft.AspNetCore.Mvc;

public class VoteController : Controller
{
    private readonly ApplicationDbContext _context;

    public VoteController(ApplicationDbContext context)
    {
        _context = context;
    }

        // GET: Vote/Create
        public IActionResult Create()
        {
            // Dohvati tipove glasanja i kandidate
            var votingTypes = _context.Types.ToList();
            var candidates = _context.Candidates.ToList();

            // Provera da li su podaci uspešno učitani
            if (votingTypes == null || candidates == null)
            {
                return View("Error");
            }

            // Preuzmi JMBG sa sesije
            var userJMBG = HttpContext.Session.GetString("UserJMBG");

            // Ako nije pronađen JMBG, korisnik nije prijavljen, pa ga vraćamo na login
            if (string.IsNullOrEmpty(userJMBG))
            {
                return RedirectToAction("Index", "Login");
            }

            // Prosledi podatke u ViewData
            ViewData["VotingTypes"] = votingTypes;
            ViewData["Candidates"] = candidates;

            // Kreiraj novi Vote objekat i postavi JMBG sa sesije
            var vote = new Vote
            {
                JMBG = userJMBG,  // Automatski popunjeno iz sesije
                DatumGlasanja = DateTime.Now // Postavi trenutni datum
            };

            return View(vote);
        }

    // POST: Vote/Create
    // POST: Vote/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("JMBG,TypeId,CandidateId")] Vote vote)
    {
        if (ModelState.IsValid)
        {
            // Proveri da li je korisnik već glasao za ovaj tip glasanja
            var existingVote = _context.Votes
                .FirstOrDefault(v => v.JMBG == vote.JMBG && v.TypeId == vote.TypeId);

            if (existingVote != null)
            {
                // Ako je već glasao za ovaj tip glasanja, obavesti ga
                TempData["Message"] = "Već ste glasali za ovaj tip glasanja!";
                TempData["MessageType"] = "error";
                return RedirectToAction("Denied");
            }

            try
            {
                // Dodaj trenutni datum glasanja
                vote.DatumGlasanja = DateTime.Now;

                // Sačuvaj glasanje u bazu podataka
                _context.Add(vote);
                _context.SaveChanges();

                // Postavi poruku o uspehu u TempData
                TempData["Message"] = "Glasanje je uspešno sprovedeno!";
                TempData["MessageType"] = "success";

                // Preusmeravanje na stranicu sa porukom o uspehu
                return RedirectToAction("Success");
            }
            catch
            {
                // U slučaju greške, postavi poruku o grešci
                TempData["Message"] = "Došlo je do greške prilikom glasanja!";
                TempData["MessageType"] = "error";
                return RedirectToAction("Denied");
            }
        }

        // Ako model nije validan, vraćamo korisnika na istu stranicu za unos
        return View(vote);
    }

    // GET: Vote/Success
    public IActionResult Success()
    {
        // Pročitaj poruku iz TempData
        ViewData["Message"] = TempData["Message"];
        ViewData["MessageType"] = TempData["MessageType"];

        return View();
    }

    // GET: Vote/Denied
    public IActionResult Denied()
    {
        // Pročitaj poruku iz TempData
        ViewData["Message"] = TempData["Message"];
        ViewData["MessageType"] = TempData["MessageType"];

        return View();
    }


}

