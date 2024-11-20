using TextbookBookstore.Data;
using Microsoft.AspNetCore.Mvc;
using TextbookBookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TextbookBookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult ListBook(string? language, string? className)
        {
                IQueryable<Book> query = _context.Books.Include(b => b.Language).Include(b => b.Class);

                if (!string.IsNullOrEmpty(language))
                {
                    query = query.Where(b => b.Language.LanguageName == language);
                }

                if (!string.IsNullOrEmpty(className))
                {
                    query = query.Where(b => b.Class.ClassName == className);
                }

                var books = query.OrderBy(b => b.Title).ToList();

                ViewBag.Languages = _context.Languages.ToList();
                ViewBag.Classes = _context.Classes.ToList();
                ViewBag.SelectedLanguage = language;
                ViewBag.SelectedClass = className;

                return View(books);
        }


        [Authorize(Roles="Admin")]
        [HttpGet]
        public IActionResult AddBook()
        {
            try
            {
                ViewBag.Action = "Add";
                ViewBag.Languages = _context.Languages.OrderBy(l => l.LanguageName).ToList();
                ViewBag.Classes = _context.Classes.OrderBy(c => c.ClassName).ToList();
                var model = new Book
                {
                    PublishedDate = DateTime.Today
                };
                return View(model);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Getting Add Book: {ex.Message}");
                return RedirectToAction("ListBook", "Book");
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook(Book book, IFormFile? BookCover)
        {
            ViewBag.Languages = _context.Languages.OrderBy(l => l.LanguageName).ToList();
            ViewBag.Classes = _context.Classes.OrderBy(c => c.ClassName).ToList();
            try
            {
                // Check if a new file is uploaded
                if (BookCover != null && BookCover.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(BookCover.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await BookCover.CopyToAsync(stream);
                    }
                    book.BookCover = "images/" + fileName;
                    ModelState.Remove("BookCover");
                }
                else if (book.BookId != 0) // Editing an existing book without uploading a new cover
                {
                    // Retrieve the existing book from the database to keep the current BookCover path
                    var existingBook = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == book.BookId);
                    book.BookCover = existingBook?.BookCover;
                    ModelState.Remove("BookCover");
                }
                
                if (ModelState.IsValid)
                {
                    if (book.BookId == 0) // Adding a new book
                    {
                        book.PublishedDate = book.PublishedDate.Date;
                        _context.Books.Add(book);
                    }
                    else // Updating an existing book
                    {
                        _context.Books.Update(book);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction("ListBook", "Book");
                }
                else
                {
                    // Log ModelState errors for debugging
                    foreach (var state in ModelState.Values)
                    {
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine(error.ErrorMessage);
                        }
                    }
                    return View(book);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding book: {ex.Message}");
                return View(book);
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditBook(int id)
        {
            try
            {
                ViewBag.Action = "Edit";
                Book book = _context.Books.Find(id);
                ViewBag.Languages = _context.Languages.OrderBy(l => l.LanguageName).ToList();
                ViewBag.Classes = _context.Classes.OrderBy(c => c.ClassName).ToList();
                return View("AddBook", book);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error HttpGet EditBook: {ex.Message}");
                return View("ListBook", "Book");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var book = _context.Books.Find(id);
                ViewBag.Languages = _context.Languages.OrderBy(l => l.LanguageName).ToList();
                ViewBag.Classes = _context.Classes.OrderBy(c => c.ClassName).ToList();
                return View(book);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error HttpGet DeleteBook: {ex.Message}");
                return View("ListBook", "Book");
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteBook(Book book)
        {
            try
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Deleting Book: {ex.Message}");
                return View(book);
            }
            
        }
        public async Task<IActionResult> DetailsBook(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var book = await _context.Books
                                        .Include(b => b.Language)
                                        .FirstOrDefaultAsync(b => b.BookId == id);

                if (book == null)
                {
                    return NotFound();
                }

                // Retrieve the user's individual status for this book
                var userBookStatus = await _context.UserBookStatuses
                                                   .FirstOrDefaultAsync(ubs => ubs.BookId == id && ubs.UserId == userId);

                var viewModel = new BookDetailsViewModel
                {
                    Book = book,
                    UserStatus = userBookStatus?.Status ?? "Not Started" // Default to "Not Started" if no status exists
                };

                return View(viewModel);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error HttpGet DetailsBook: {ex.Message}");
                return View("ListBook", "Book");
            }
            
        }

        [HttpGet]
        public IActionResult BookStatus(int id)
        {
            try
            {
                var book = _context.Books.Find(id);
                if (book == null)
                {
                    return NotFound();
                }

                return View(book);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error HttpGet BookStatus: {ex.Message}");
                return View("ListBook", "Book");
            }

        }
        [HttpPost]
        public async Task<IActionResult> BookStatus(int id, string BookStatus)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userBookStatus = await _context.UserBookStatuses
                    .FirstOrDefaultAsync(ubs => ubs.BookId == id && ubs.UserId == userId);

                if (userBookStatus == null)
                {
                    userBookStatus = new UserBookStatus
                    {
                        BookId = id,
                        UserId = userId,
                        Status = BookStatus
                    };
                    _context.UserBookStatuses.Add(userBookStatus);
                }
                else
                {
                    userBookStatus.Status = BookStatus;
                    _context.UserBookStatuses.Update(userBookStatus);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("DetailsBook", new { id });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Altering Book Status: {ex.Message}");
                return View("ListBook", "Book");
            }

        }

    }
}
