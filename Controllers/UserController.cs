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
        public IActionResult Login(IFormCollection form)
        {
            try
            {
                var dbUser = _context.Users.FirstOrDefault(u => u.Email == form["Email"].ToString());
                if (dbUser == null)
                {
                    ViewBag.Error = "No user found";
                    return View();
                }
                if (dbUser.Password!.Equals(form["Password"]))
                {
                    ViewBag.Error = "";
                    HttpContext.Response.Cookies.Append("logged_in", "true");
                    HttpContext.Response.Cookies.Append("current_user_id", dbUser.Id.ToString());
                    HttpContext.Response.Cookies.Append("current_user_email", dbUser.Email!);
                    HttpContext.Response.Cookies.Append("current_user_name", dbUser.Name!);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Username or Password Incorrect";
                    return View();
                }

            }
            catch (Exception e)
            {
                ViewBag.Error = "Some error occured";
                Console.WriteLine(e.Message);
                return View();
            }
        }

        public IActionResult Register() { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Register(IFormCollection form)
        {
            await Console.Out.WriteLineAsync("Hit");
            try
            {
                UserModel user = new()
                {
                    Name = form["Name"].ToString(),
                    Email = form["Email"].ToString(),
                    Password = form["Password"].ToString(),
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                var dbUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                HttpContext.Response.Cookies.Append("logged_in", "true");
                HttpContext.Response.Cookies.Append("current_user_id", dbUser.Id.ToString());
                HttpContext.Response.Cookies.Append("current_user_email", dbUser.Email!);
                HttpContext.Response.Cookies.Append("current_user_name", dbUser.Name!);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.Error = "Some error occured";
                Console.WriteLine(e.Message);
                return View();
            }
        }
    }
}
