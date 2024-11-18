using TextbookBookstore.Data;

namespace TextbookBookstore.Models
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly ISession _session;

        public CartService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public List<CartItem> GetCart()
        {
            var cart = _session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return cart;
        }

        public void AddToCart(int bookId, int quantity)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(c => c.BookId == bookId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                var book = _context.Books.Find(bookId);
                if (book != null)
                {
                    cart.Add(new CartItem { BookId = bookId, Book = book, Quantity = quantity });
                }
            }

            _session.SetObjectAsJson("Cart", cart);
        }

        public void ClearCart()
        {
            _session.Remove("Cart");
        }
        public void RemoveFromCart(int bookId)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(c => c.BookId == bookId);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                _session.SetObjectAsJson("Cart", cart); // Update the session storage
            }
        }

    }

}
