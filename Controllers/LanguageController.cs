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
            var query = context.Languages.ToList();
            return View(query);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddLanguage()
        {
            ViewBag.Action = "Add";
            return View(new Language());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditLanguage(int id)
        {
            var query = context.Languages.Find(id);
            return View("AddLanguage", query);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddLanguage(Language language)
        {
            if (ModelState.IsValid)
            {
                if(language.LanguageId == 0)
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteLanguage(int id)
        {
            var query = context.Languages.Find(id);
            return View(query);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteLanguage(Language language)
        {
            context.Languages.Remove(language);
            context.SaveChanges();
            return RedirectToAction("Index", "Language");
        }
}
}
