using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_glasanje.Models
{
    public class User
    {

        [Key]
        [Required(ErrorMessage = "JMBG je obavezan.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG mora imati tačno 13 cifara.")]
        public string JMBG { get; set; }  // Jedinstveni identifikator korisnika

        [Required(ErrorMessage = "Ime je obavezno.")]
        [MaxLength(50)]
        public string Ime { get; set; }  // Ime korisnika

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [MaxLength(50)]
        public string Prezime { get; set; }  // Prezime korisnika

        [Required(ErrorMessage = "Datum rodjenja je obavezan.")]
        public DateTime DatumRodjenja { get; set; }  // Datum rođenja korisnika

        [Required(ErrorMessage = "Kontakt telefon je obavezan.")]
        public string Telefon { get; set; }  // Broj telefona korisnika

        [Required]
        public string Sifra { get; private set; }  // Sistem kreira lozinku (sifra)

        public User()
        {
            // Generisanje lozinke prilikom registracije
            Sifra = GenerisiSifru();
        }

        private string GenerisiSifru()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
