using TextbookBookstore.Models;
using Microsoft.AspNetCore.Mvc;
using TextbookBookstore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TextbookBookstore.Controllers
{
    public class LanguageController : Controller
    {
        public ApplicationDbContext context { get; set; }
        public LanguageController(ApplicationDbContext ctx) => context = ctx;
        public IActionResult Index()
        {
            try
            {
                var query = context.Languages.ToList();
                return View(query);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Opening Language Index: {ex.Message}");
                return RedirectToAction("ListBook", "Book");
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddLanguage()
        {
            try
            {
                ViewBag.Action = "Add";
                return View(new Language());
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Getting Add Language: {ex.Message}");
                return RedirectToAction("Index", "Language");
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditLanguage(int id)
        {
            try
            {
                var query = context.Languages.Find(id);
                return View("AddLanguage", query);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Getting Edit Language: {ex.Message}");
                return RedirectToAction("Index", "Language");
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddLanguage(Language language)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (language.LanguageId == 0)
                    {
                        context.Languages.Add(language);
                    }
                    else
                    {
                        context.Languages.Update(language);
                    }
                    context.SaveChanges();
                    return RedirectToAction("Index", "Language");
                }
                else
                {
                    return View(language);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Adding Language To DB: {ex.Message}");
                return View(language);
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteLanguage(int id)
        {
            try
            {
                var query = context.Languages.Find(id);
                return View(query);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Getting Delete Language: {ex.Message}");
                return RedirectToAction("Index", "Language");
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteLanguage(Language language)
        {
            try
            {
                context.Languages.Remove(language);
                context.SaveChanges();
                return RedirectToAction("Index", "Language");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Deleting Language From DB: {ex.Message}");
                return View(language);
            }
            
        }
}
}
