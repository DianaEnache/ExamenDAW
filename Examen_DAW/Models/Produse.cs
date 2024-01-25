using System.ComponentModel.DataAnnotations;

namespace Examen_DAW.Models
{
    public class Produse
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "A text is required")]
        [StringLength(50, ErrorMessage = "The text should have at most 50 characters")]
        [MinLength(1, ErrorMessage = "The text should have at least 1 characters")]
        public string? NumeProdus { get; set; }

        public virtual ICollection<Comenzi>? Comenzi { get; set; }
    }
}
