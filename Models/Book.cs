using TextbookBookstore.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextbookBookstore.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "The Author field is required.")]
        public string Author { get; set; } = string.Empty;
        [Required(ErrorMessage = "The Price field is required.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "The Published Date field is required.")]
        public DateTime PublishedDate { get; set; }
        public string BookStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please upload the cover of the book")]
        public string? BookCover { get; set; }
        [Required(ErrorMessage = "Please include a description of the book.")]
        public string? BookDescription { get; set; }
        [ValidateNever]
        public Language Language { get; set; } = null!;
        [Required(ErrorMessage = "Please select a language")]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }
    }
}
