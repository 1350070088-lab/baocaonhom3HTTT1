using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Cần dòng này để dùng CountAsync
using DatingWebb.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebb.ViewComponents
{
    public class AdminNotificationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AdminNotificationViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Sử dụng CountAsync thay vì Count để không làm nghẽn luồng xử lý (Non-blocking)
            // Logic: Đếm những User mới mà Admin chưa đọc
            var count = await _context.AppUsers
                                     .Where(u => !u.IsReadByAdmin)
                                     .CountAsync();

            // Trả số lượng này về file Default.cshtml trong thư mục Components
            return View(count); 
        }
    }
}