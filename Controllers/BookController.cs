using TextbookBookstore.Data;
using Microsoft.AspNetCore.Mvc;
using TextbookBookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TextbookBookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult ListBook(string? language)
        {
            IQueryable<Book> query = _context.Books.Include(b => b.Language);
            if (!string.IsNullOrEmpty(language))
            {
                query = query.Where(b => b.Language.LanguageName == language);
            }
            var books = query.OrderBy(b => b.Title).ToList();
            ViewBag.Languages = _context.Languages.OrderBy(l => l.LanguageName).ToList();
            ViewBag.SelectedLanguage = language;
            return View(books);
        }
        [HttpGet]
        public IActionResult AddBook()
        {
            ViewBag.Action = "Add";
            ViewBag.Languages = _context.Languages.OrderBy(l => l.LanguageName).ToList();
            var model = new Book
            {
                PublishedDate = DateTime.Today
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(Book book, IFormFile? BookCover)
        {
            ViewBag.Languages = _context.Languages.OrderBy(l => l.LanguageName).ToList();
            try
            {
                if (ModelState.IsValid)
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
                        book.BookCover = "/images/" + fileName;
                    }
                    else if (book.BookId != 0) // Editing an existing book without uploading a new cover
                    {
                        // Retrieve the existing book from the database to keep the current BookCover path
                        var existingBook = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == book.BookId);
                        book.BookCover = existingBook?.BookCover;
                    }

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
                    return View(book);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding book: {ex.Message}");
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult EditBook(int id)
        {
            ViewBag.Action = "Edit";
            Book book = _context.Books.Find(id);
            ViewBag.Languages = _context.Languages.OrderBy(l => l.LanguageName).ToList();
            return View("AddBook", book);
        }
        [HttpGet]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            ViewBag.Languages = _context.Languages.OrderBy(l => l.LanguageName).ToList();
            return View(book);
        }
        [HttpPost]
        public IActionResult DeleteBook(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DetailsBook(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == id);
            return View(book);
        }
        public ActionResult FilterLanguage(string id)
        {
            string languageType = ViewBag.LanguageType;
            string textFilter = id;
            var books = from b in _context.Books select b;
            books = books.Where(b => b.Language.LanguageName.Equals(languageType));
            return RedirectToAction("List", "Book");
        }


    }
}
