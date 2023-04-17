using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Text_Editor.Models;

namespace Text_Editor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var isLoggedIn = Request.Cookies["logged_in"];
            if (isLoggedIn == null || isLoggedIn == "false")
            {
                return RedirectToAction("Login", "User");
            }
            var id = Request.Cookies["current_user_id"];
            var docs = await _context.Docs.Where(d => d.UserId == Convert.ToInt32(id)).ToListAsync();
            ViewBag.docs = docs;
            ViewBag.name = Request.Cookies["current_user_name"];
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            DocModel doc = new()
            {
                DocName = form["DocName"].ToString(),
                DocContent = form["DocContent"].ToString(),
                IsSharable = "false",
                UserId = Convert.ToInt32(Request.Cookies["current_user_id"])
            };
            _context.Docs.Add(doc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
