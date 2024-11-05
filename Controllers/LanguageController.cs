using TextbookBookstore.Models;
using Microsoft.AspNetCore.Mvc;
using TextbookBookstore.Data;

namespace TextbookBookstore.Controllers
{
    public class LanguageController : Controller
    {
        public ApplicationDbContext context { get; set; }
        public LanguageController(ApplicationDbContext ctx) => context = ctx;
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddLanguage()
        {
            return View(new Language());
        }
        [HttpPost]
        public IActionResult AddLanguage(Language language)
        {
            if (ModelState.IsValid)
            {
                context.Languages.Add(language);
                context.SaveChanges();
                return RedirectToAction("ListBook", "Book");
            }
            else
            {
                return View(language);
            }

        }
    }
}
