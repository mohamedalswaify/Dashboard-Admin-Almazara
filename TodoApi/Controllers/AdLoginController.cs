using AdminControl.Controllers;
using AdminControl.Models;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    public class AdLogin : Controller
    {
        private readonly BloggingContext _context;
        public AdLogin(BloggingContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Employee employee)
        {
            var emp=_context.Employees.FirstOrDefault(b=>b.Email == employee.Email.ToLower() && b.Password==employee.Password);
            if (emp != null)
            {
                var EmployeeName = _context.Employees.FirstOrDefault(e => e.Email == employee.Email);

                DateTime expiryTime = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("EmployeeId", employee.Id.ToString(), new CookieOptions
                {
                    Expires = expiryTime,
                    HttpOnly = false,
                });
                Response.Cookies.Append("EmployeeEmail", employee.Email, new CookieOptions
                {
                    Expires = expiryTime,
                    HttpOnly = true
                });
                Response.Cookies.Append("EmployeeName", EmployeeName.Name, new CookieOptions
                {
                    Expires = expiryTime,
                    HttpOnly = false,
                });
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["EmailVal"] = employee.Email;
                TempData["Block"] = "block";
                return RedirectToAction("Login");
            }
        }
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("EmployeeId");
            Response.Cookies.Delete("EmployeeName");
            Response.Cookies.Delete("EmployeeEmail");
            return RedirectToAction(nameof(Login));
        }
    }
}
