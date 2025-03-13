using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_glasanje.Models
{
    public class Candidate
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int TypeId { get; set; }  // Tip glasanja na koji kandidat pripada (strani ključ na Type)

        [ForeignKey("TypeId")]
        public Type Type { get; set; }  // Navigaciono svojstvo na Type

        [Required]
        public int CandidateNumber { get; set; }  // Broj kandidata (npr. 1, 2, 3...)

        [Required]
        [MaxLength(100)]
        public string CandidateName { get; set; }  // Ime kandidata
    }
}
