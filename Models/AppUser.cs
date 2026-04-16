using System;
using System.ComponentModel.DataAnnotations;

namespace DatingWebb.Models
{
    public class AppUser
    {
        [Key] // Đánh dấu đây là Khóa chính
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // CÁI NÀY QUAN TRỌNG: Thêm ImageUrl để lưu link ảnh đại diện
        // Dấu ? để nó không bị lỗi nếu người dùng chưa kịp có ảnh
        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Trạng thái để Admin quản lý: false là mới, true là đã xem
        public bool IsReadByAdmin { get; set; } = false; 
    }
}