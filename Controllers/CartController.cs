using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using TextbookBookstore.Data;
using TextbookBookstore.Models;

namespace TextbookBookstore.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(CartService cartService, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _cartService = cartService;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult IndexCart()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            _cartService.AddToCart(id, quantity);
            return RedirectToAction("ListBook", "Book");
        }
        public async Task<IActionResult> SubmitOrder()
        {
            var cart = _cartService.GetCart();
            if(cart.Count == 0)
            {
                return RedirectToAction("Index");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.Now
            };
            foreach(var item in cart)
            {
                var book = await _context.Books.FindAsync(item.BookId);
                if(book != null)
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        BookId = item.BookId,
                        Quantity = item.Quantity,
                        Price = book.Price
                    });
                }
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            _cartService.ClearCart();
            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return RedirectToAction("Index");
            }

            return View(order);
        }

        public async Task<IActionResult> PriorOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = await _context.Orders
                                .Where(o => o.UserId == user.Id)
                                .Include(o => o.OrderDetails)
                                .ThenInclude(od => od.Book)
                                .OrderByDescending(o => o.OrderDate)
                                .ToListAsync();
            return View(orders);
        }

    }
}
