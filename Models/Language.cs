using System.ComponentModel.DataAnnotations;

namespace TextbookBookstore.Models
{
    public class Language
    {
        public int LanguageId { get; set; }
        [Required(ErrorMessage = "Please enter a valid language name")]
        public string LanguageName { get; set; } = string.Empty;
    }
}
