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
            try
            {
                var cart = _cartService.GetCart();
                return View(cart);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error with Index Cart: {ex.Message}");
                return RedirectToAction("ListBook", "Book");
            }
            
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            try
            {
                _cartService.AddToCart(id, quantity);
                return RedirectToAction("ListBook", "Book");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Adding Item To Cart: {ex.Message}");
                return RedirectToAction("ListBook", "Book");
            }
            
        }
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                _cartService.ClearCart();
                _context.SaveChanges();
                return RedirectToAction("IndexCart", "Cart");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Clearing Cart: {ex.Message}");
                return RedirectToAction("IndexCart", "Cart");
            }
            
        }
        public IActionResult RemoveFromCart(int id)
        {
            try
            {
                _cartService.RemoveFromCart(id);
                return RedirectToAction("IndexCart");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Removing From Cart: {ex.Message}");
                return RedirectToAction("IndexCart", "Cart");
            }
            
        }


        public async Task<IActionResult> SubmitOrder()
        {
            try
            {
                var cart = _cartService.GetCart();
                if (cart.Count == 0)
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
                foreach (var item in cart)
                {
                    var book = await _context.Books.FindAsync(item.BookId);
                    if (book != null)
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
            catch(Exception ex)
            {
                Console.WriteLine($"Error Submitting Order: {ex.Message}");
                return RedirectToAction("IndexCart", "Cart");
            }
            
        }
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            try
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
            catch(Exception ex)
            {
                Console.WriteLine($"Error Creating Order Confirmation {ex.Message}");
                return RedirectToAction("IndexCart", "Cart");
            }
            
        }

        public async Task<IActionResult> PriorOrders()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
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
            catch(Exception ex)
            {
                Console.WriteLine($"Error With Prior Orders: {ex.Message}");
                return RedirectToAction("ListBook", "Book");
            }
            
        }

    }
}
