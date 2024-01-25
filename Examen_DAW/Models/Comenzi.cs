using System.ComponentModel.DataAnnotations;

namespace Examen_DAW.Models
{
    public class Comenzi
    {

        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "A text is required")]
        [StringLength(50, ErrorMessage = "The text should have at most 50 characters")]
        [MinLength(1, ErrorMessage = "The text should have at least 1 characters")]
        public string? Nume{ get; set; }

        [Required(ErrorMessage = "A content is required")]
        [StringLength(100, ErrorMessage = "The content should have at most 50 characters")]
        [MinLength(1, ErrorMessage = "The content should have at least 1 characters")]
        public string? Adresa { get; set; }

        [Required(ErrorMessage = "An adress is required")]
        public int? ProduseId { get; set; }

        public virtual Produse? Produse { get; set; }
    }
}
