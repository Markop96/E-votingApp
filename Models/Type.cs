using System.ComponentModel.DataAnnotations;

namespace E_glasanje.Models
{
    public class Type
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }  // Tip glasanja (npr. "Predsednički", "Parlamentarni", itd.)
    }
}
