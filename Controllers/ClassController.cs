using TextbookBookstore.Models;
using Microsoft.AspNetCore.Mvc;
using TextbookBookstore.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace TextbookBookstore.Controllers
{
    public class ClassController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var query = _context.Classes.ToList();
            return View(query);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddClass()
        {
            ViewBag.Action = "Add";
            return View(new Class());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditClass(int id)
        {
            var query = _context.Classes.Find(id);
            return View("AddClass", query);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddClass(Class classes)
        {
            if (ModelState.IsValid)
            {
                if (classes.ClassId == 0)
                {
                    _context.Classes.Add(classes);
                }
                else
                {
                    _context.Classes.Update(classes);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Class");
            }
            else
            {
                return View(classes);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteClass(int id)
        {
            var query = _context.Classes.Find(id);
            return View(query);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteClass(Class classes)
        {
            _context.Classes.Remove(classes);
            _context.SaveChanges();
            return RedirectToAction("Index", "Class");
        }
    }

}
