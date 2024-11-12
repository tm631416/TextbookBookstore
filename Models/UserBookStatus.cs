using Microsoft.AspNetCore.Identity;

namespace TextbookBookstore.Models
{
    public class UserBookStatus
    {
        public int UserBookStatusId { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public string Status { get; set; }



    }
}
