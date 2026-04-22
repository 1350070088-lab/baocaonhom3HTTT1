using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatingWebb.Models
{
    public class AppUser
    {
        [Key] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        // --- BỔ SUNG CÁC TRƯỜNG CÒN THIẾU ---

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? PhoneNumber { get; set; } // Hiển thị ở bảng Admin

        public string? Bio { get; set; } // Lưu nội dung "Tự truyện"

        public bool IsVip { get; set; } = false; // Trạng thái "Thành viên PRO"

        public bool IsActive { get; set; } = false; // Trạng thái "ĐÃ DUYỆT" hoặc "CHỜ DUYỆT"

        // Link ảnh mặc định hoặc ảnh từ internet
        public string? ImageUrl { get; set; }

        // Lưu trữ ảnh thực tế dưới dạng byte để có thể đổi ảnh tùy ý
        public byte[]? PhotoData { get; set; } 

        // ------------------------------------

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public bool IsReadByAdmin { get; set; } = false; 

        // Mối quan hệ với bảng UserMatch (Để biết ai đã quẹt ai)
        public virtual ICollection<UserMatch>? SentMatches { get; set; }
        public virtual ICollection<UserMatch>? ReceivedMatches { get; set; }

        // Danh sách các Payment (Nếu m có làm chức năng Nâng cấp VIP)
        public virtual ICollection<Payment>? Payments { get; set; }
    }
}
