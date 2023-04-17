using AppointmentScheduler.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Text_Editor.Models;

namespace Text_Editor.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] UserModel userModel)
        {
            Console.Write("Hit");
            var user = await _context.Users.FindAsync(Request.Form["Email"].ToString());
            if (user == null)
            {
                return BadRequest(new
                {
                    status = false,
                    message = "No user found"
                });
            }

            if (!BCryptUtils.VerifyPassword(Request.Form["Password"].ToString(), user.Password!))
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Password not matched"
                });
            }
            Response.Redirect("/");
            return View();
        }

        public IActionResult Register() { 
            return View(); 
        }
    }
}
