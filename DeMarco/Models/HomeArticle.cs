using System.ComponentModel.DataAnnotations;

namespace DeMarco.Models
{
    public class HomeArticle
    {
        public int Id { get; set; }

        [Display(Name = "Titulek")]
        [Required(ErrorMessage = "Vyplňte titulek")]
        [StringLength(100, ErrorMessage = "Titulek je příliš dlouhý")]
        public string Title { get; set; } = "";

        [Display(Name = "Popis")]
        [Required(ErrorMessage = "Vyplňte obsah")]
        public string Content { get; set; } = "";
    }
}