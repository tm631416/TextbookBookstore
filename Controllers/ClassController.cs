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
            try
            {
                var query = _context.Classes.ToList();
                return View(query);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Getting Class List: {ex.Message}");
                return RedirectToAction("ListBook", "Book");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddClass()
        {
            try
            {
                ViewBag.Action = "Add";
                return View(new Class());
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Getting Add Class: {ex.Message}");
                return RedirectToAction("Index", "Class");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditClass(int id)
        {
            try
            {
                var query = _context.Classes.Find(id);
                return View("AddClass", query);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Getting Edit Class: {ex.Message}");
                return RedirectToAction("Index", "Class");
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddClass(Class classes)
        {
            try
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
            catch(Exception ex)
            {
                Console.WriteLine($"Error Adding Class To Db: {ex.Message}");
                return View(classes);
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteClass(int id)
        {
            try
            {
                var query = _context.Classes.Find(id);
                return View(query);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Getting DeleteClass: {ex.Message}");
                return RedirectToAction("Index", "Class");
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteClass(Class classes)
        {
            try
            {
                _context.Classes.Remove(classes);
                _context.SaveChanges();
                return RedirectToAction("Index", "Class");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Deleting Class From DB: {ex.Message}");
                return View(classes);
            }
            
        }
    }

}
