using Microsoft.AspNetCore.Mvc;
using DatingWebb.Data;
using DatingWebb.Models;
using System.Threading.Tasks;

namespace DatingWebb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Kết nối Database vào Controller
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Trang Đăng ký (Hiển thị Form)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Xử lý khi bấm nút "Tạo mới"
        [HttpPost]
        public async Task<IActionResult> Register(AppUser user)
        {
            if (ModelState.IsValid)
            {
                // 1. Thêm user vào bảng AppUsers trong Database
                _context.AppUsers.Add(user);
                
                // 2. Lưu lại thay đổi
                await _context.SaveChangesAsync();

                // 3. Đăng ký xong thì chuyển sang trang Matches để chọn "ghệ"
                return RedirectToAction("Matches", "Home");
            }

            // Nếu lỗi thì ở lại trang Register
            return View(user);
        }
    }
}