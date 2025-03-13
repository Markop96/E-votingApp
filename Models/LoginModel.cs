using System.ComponentModel.DataAnnotations;

namespace E_glasanje.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG mora imati tačno 13 cifara.")]
        public string JMBG { get; set; }  // JMBG korisnika

        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Lozinka mora imati 8 karaktera.")]
        public string Sifra { get; set; }  // Lozinka koju je sistem generisao
    }
}
