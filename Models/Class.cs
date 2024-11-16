using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;

namespace TextbookBookstore.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        [Required(ErrorMessage = "Please enter a class name")]
        public string ClassName { get; set; } = string.Empty;
    }
}
