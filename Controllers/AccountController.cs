using Microsoft.AspNetCore.Mvc;
using DatingWebb.Data;
using DatingWebb.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; 
using System.Linq; // CẦN THIẾT: Để sử dụng hàm FirstOrDefault trong Login

namespace DatingWebb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUser user)
        {
            if (ModelState.IsValid)
            {
                // 1. Thêm user vào database
                _context.AppUsers.Add(user);
                await _context.SaveChangesAsync();

                // 2. LÀM SẠCH VÀ LƯU MỚI SESSION
                // Đảm bảo tên cũ (nếu có) bị xóa sạch trước khi lưu tên mới từ form
                HttpContext.Session.Clear(); 
                HttpContext.Session.SetString("UserFullName", user.FullName);

                // 3. Chuyển sang trang Matches
                return RedirectToAction("Matches", "Home");
            }

            return View(user);
        }

        // Hàm Login xử lý đăng nhập thực tế
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Kiểm tra email và password trong bảng AppUsers
            var user = _context.AppUsers.FirstOrDefault(u => u.Email == email && u.Password == password);
            
            if (user != null)
            {
                // Nếu tìm thấy, lưu tên thật vào Session để Sidebar hiển thị
                HttpContext.Session.SetString("UserFullName", user.FullName);
                return RedirectToAction("Discover", "Home");
            }

            // Nếu sai thông tin, báo lỗi (Mày có thể thêm TempData để hiện thông báo lỗi)
            ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác.");
            return View();
        }

        // Bổ sung hàm Logout để người dùng có thể thoát và xóa tên trên Sidebar
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}




