using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DeMarco.Models
{
    public class Pasta
    {
        public int Id { get; set; }

        [Display(Name = "Číslo")]
        [Required(ErrorMessage = "Vyplňte obsah")]
        public int NumberItem { get; set; }

        [NotMapped]
        public byte[] ImageByte { get; set; } = { 1, 2, 3 };

        [Display(Name = "Obrázek")]
        public string ImagePath { get; set; } = "";

        [Display(Name = "Název")]
        [Required(ErrorMessage = "Vyplňte obsah")]
        public string Title { get; set; } = "";

        [Display(Name = "Váha")]
        [Required(ErrorMessage = "Vyplňte obsah")]
        public string Weight { get; set; } = "";

        [Display(Name = "Popis")]
        [Required(ErrorMessage = "Vyplňte popisek")]
        public string Description { get; set; } = "";

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Vyplňte obsah")]
        public string Price { get; set; } = "";

        public bool IsHidden { get; set; }

        /// <summary>
        /// save image from byte[] to *.jpg, save to it images folder and save path 
        /// </summary>
        public void SaveImageAndMapPath()
        {
            File.WriteAllBytes(@$"wwwroot\images\{Title}.jpg", ImageByte);
            ImagePath = @$"images\{Title}.jpg";
        }
    }
}