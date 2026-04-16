using System.ComponentModel.DataAnnotations;

namespace DatingWebb.Models
{
    public class UserMatch
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Đã tương hợp";

        // Gợi ý: M nên để string.Empty thay vì dấu ? để tránh lỗi Null Reference khi hiển thị giao diện
        public string Bio { get; set; } = string.Empty; 
    }
}