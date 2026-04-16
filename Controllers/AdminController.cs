using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingWebb.Data;
using DatingWebb.Models;
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
            // 1. Tổng thành viên (Bảng này đã có nên chạy OK)
            ViewBag.TotalUsers = await _context.AppUsers.CountAsync();

            // 2. Thành viên mới (Chưa xem)
            ViewBag.NewUsers = await _context.AppUsers.CountAsync(u => !u.IsReadByAdmin);

            // --- QUAN TRỌNG: Tạm thời gán bằng 0 để vượt qua lỗi Build CS1061 ---
            // M phải gán cứng như vầy thì mới chạy được lệnh 'dotnet ef migrations add'
            ViewBag.DailyMatches = 0; 
            ViewBag.Revenue = 0;

            /* SAU KHI m đã chạy thành công 2 lệnh:
               1. dotnet ef migrations add AddMatchesAndPayments
               2. dotnet ef database update
               THÌ m mới được mở (uncomment) đoạn code dưới đây ra nhé:

               var today = DateTime.Today;
               ViewBag.DailyMatches = await _context.Matches.CountAsync(m => m.MatchedAt >= today); 

               ViewBag.Revenue = await _context.Payments
                                .Where(p => p.Status == "Success")
                                .SumAsync(p => (decimal?)p.Amount) ?? 0;
            */

            // Lấy danh sách user để đổ vào Table
            var users = await _context.AppUsers
                                    .OrderByDescending(u => u.CreatedAt)
                                    .ToListAsync();
            
            return View(users);
        }

        // Chức năng đánh dấu đã xem
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var user = await _context.AppUsers.FindAsync(id);
            if (user != null)
            {
                user.IsReadByAdmin = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}