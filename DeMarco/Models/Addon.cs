using System.ComponentModel.DataAnnotations;

namespace DeMarco.Models
{
    public class Addon
    {
        public int Id { get; set; }

        [Display(Name = "Název")]
        [Required(ErrorMessage = "Vyplňte obsah")]
        public string Title { get; set; } = "";

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Vyplňte obsah")]
        public string Price { get; set; } = "";

        public bool IsHidden { get; set; }
    }
}