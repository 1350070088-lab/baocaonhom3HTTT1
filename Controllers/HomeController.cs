using Microsoft.AspNetCore.Mvc;
using DatingWebb.Models;
using DatingWebb.Data; 
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; 
using System.Threading.Tasks; 
using System;

namespace DatingWebb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void UpdateNotificationCount()
        {
            if (_context.UserMatches != null)
            {
                ViewBag.NotificationCount = _context.UserMatches.Count(m => m.IsActive);
            }
            else
            {
                ViewBag.NotificationCount = 0;
            }
        }

        // QUY TRÌNH 1: Mở web mặc định vào trang Đăng ký
        public IActionResult Index()
        {
            return RedirectToAction("Register");
        }

        // --- TRANG ĐĂNG KÝ ---
        public IActionResult Register()
        {
            UpdateNotificationCount();
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new AppUser 
                { 
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password, 
                    CreatedAt = DateTime.Now,
                    IsActive = true 
                };

                _context.AppUsers.Add(newUser);
                await _context.SaveChangesAsync();

                // GHI NHỚ Tên đăng ký vào Session để hiển thị ở góc phải
                HttpContext.Session.SetString("UserName", newUser.FullName);
                HttpContext.Session.SetInt32("UserId", newUser.Id);
                
                // QUY TRÌNH 2: Đăng ký thành công thì vào thẳng trang người dùng (Khám phá)
                return RedirectToAction("Discover");
            }
            return View(model);
        }

        // --- CÁC TRANG DÀNH CHO NGƯỜI DÙNG ---

        public IActionResult Discover() 
        {
            UpdateNotificationCount();
            var users = _context.AppUsers.ToList(); 
            return View(users);
        }

        public IActionResult Matches() 
        {
            UpdateNotificationCount();
            var matches = _context.UserMatches.Where(m => m.IsActive).ToList();
            return View(matches);
        }

        public IActionResult Messages() 
        {
            UpdateNotificationCount();
            return View(); 
        }

        public IActionResult Activity() 
        {
            UpdateNotificationCount();
            return View(); 
        }

        public async Task<IActionResult> Profile() 
        {
            UpdateNotificationCount();
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Register");

            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == userId);
            var model = new UserProfileViewModel
            {
                Bio = user?.Bio ?? "",
                ExistingPhotoUrl = user?.ImageUrl ?? "/images/default-avatar.png"
            };
            return View(model);
        }

        public IActionResult Settings() 
        {
            UpdateNotificationCount();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { 
                RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}
