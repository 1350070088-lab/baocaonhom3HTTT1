using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingWebb.Data;
using DatingWebb.Models;
using Microsoft.AspNetCore.Http; 
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Trang quản lý danh sách người dùng
        public async Task<IActionResult> Index()
        {
            // --- BƯỚC 1: KIỂM TRA QUYỀN TRUY CẬP ---
            var displayName = HttpContext.Session.GetString("UserName");
            // Nếu không có session hoặc tên không chứa "Admin" thì đá về trang Discover
            if (string.IsNullOrEmpty(displayName) || !displayName.ToLower().Contains("admin"))
            {
                return RedirectToAction("Discover", "Home");
            }

            // --- BƯỚC 2: ĐỔ DỮ LIỆU THỐNG KÊ LÊN DASHBOARD ---
            // Đếm tổng số thành viên
            ViewBag.TotalUsers = await _context.AppUsers.CountAsync();
            
            // Đếm số user mới (chưa được Admin xem qua)
            ViewBag.NewUsers = await _context.AppUsers.CountAsync(u => !u.IsReadByAdmin);
            
            // Lấy tạm số lượng cặp đôi (Ví dụ logic: 0 cho đến khi mày làm phần Matches)
            ViewBag.DailyMatches = 0; 
            
            // Doanh thu (Mặc định 0đ như trong hình thiết kế của mày)
            ViewBag.Revenue = "0đ";

            // --- BƯỚC 3: LẤY DANH SÁCH USER ĐỂ ĐỔ VÀO BẢNG ---
            var users = await _context.AppUsers
                                    .OrderByDescending(u => u.CreatedAt)
                                    .ToListAsync();
            
            return View(users);
        }

        // Chức năng Đánh dấu đã xem (Nút hình con mắt trên giao diện)
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var displayName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(displayName) || !displayName.ToLower().Contains("admin"))
            {
                return Unauthorized();
            }

            var user = await _context.AppUsers.FindAsync(id);
            if (user != null)
            {
                user.IsReadByAdmin = true;
                await _context.SaveChangesAsync();
            }
            // Sau khi sửa xong thì load lại trang Index để cập nhật bảng
            return RedirectToAction(nameof(Index));
        }

        // Chức năng Xóa người dùng (Nút hình vòng tròn gạch chéo)
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var displayName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(displayName) || !displayName.ToLower().Contains("admin"))
            {
                return Unauthorized();
            }

            var user = await _context.AppUsers.FindAsync(id);
            if (user != null)
            {
                _context.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Hàm đăng xuất dành riêng cho Admin
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
