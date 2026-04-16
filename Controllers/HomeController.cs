using Microsoft.AspNetCore.Mvc;
using DatingWebb.Models;
using DatingWebb.Data; 
using System.Diagnostics;
using System.Linq;

namespace DatingWebb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        // 1. KẾT NỐI DATABASE
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hàm hỗ trợ dùng chung để đếm thông báo (đếm từ bảng UserMatches)
        private void UpdateNotificationCount()
        {
            // Đảm bảo tên bảng là UserMatches để khớp với DbContext
            ViewBag.NotificationCount = _context.UserMatches.Count(m => m.IsActive);
        }

        public IActionResult Index() 
        {
            UpdateNotificationCount();
            return View();
        }

        // 2. TRANG KHÁM PHÁ (Discover)
        public IActionResult Discover() 
        {
            UpdateNotificationCount();
            
            // SỬA TẠI ĐÂY: Lấy danh sách từ bảng AppUsers để xem người dùng mới
            // Chứ không lấy từ UserMatches (bảng này chỉ chứa các lượt đã match)
            var users = _context.AppUsers.ToList(); 
            return View(users);
        }

        // 3. ACTION XỬ LÝ KHI BẤM NÚT TIM
        [HttpPost]
        public IActionResult LikeUser(int id)
        {
            // Tìm trong bảng UserMatches xem đã có lượt tương tác chưa
            var match = _context.UserMatches.FirstOrDefault(m => m.User2Id == id);
            if (match != null)
            {
                match.IsActive = true; 
                _context.SaveChanges();
            }
            else 
            {
                // Nếu chưa có thì tạo mới một lượt match (giả sử user hiện tại là ID 1)
                var newMatch = new Match { User1Id = 1, User2Id = id, MatchedAt = DateTime.Now, IsActive = true };
                _context.UserMatches.Add(newMatch);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Discover");
        }

        public IActionResult Matches() 
        {
            UpdateNotificationCount();
            
            // Lấy danh sách những người đã match thành công
            var matches = _context.UserMatches.Where(m => m.IsActive).ToList();
            return View(matches);
        }

        public IActionResult Activity()
        {
            UpdateNotificationCount();
            return View();
        }

        public IActionResult Messages(string id) 
        {
            UpdateNotificationCount();
            ViewBag.CurrentChat = id;
            return View();
        }

        public IActionResult Profile() 
        {
            UpdateNotificationCount();
            return View();
        }

        public IActionResult Settings() 
        {
            UpdateNotificationCount();
            return View();
        }

        public IActionResult Register()
        {
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