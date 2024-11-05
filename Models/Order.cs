using Microsoft.AspNetCore.Identity;

namespace TextbookBookstore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
