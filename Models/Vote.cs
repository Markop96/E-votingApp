using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_glasanje.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }  // Primarni ključ

        [Required]
        [Column("UserJMBG")]
        public string JMBG { get; set; }  // JMBG korisnika koji glasa

        [Required]
        public int TypeId { get; set; }  // Tip glasanja (strani ključ ka Type)

        [Required]
        public int CandidateId { get; set; }  // Kandidat za kojeg se glasa (strani ključ ka Candidate)

        public DateTime DatumGlasanja { get; set; }  // Datum kada je glasanje izvršeno
    }
}
