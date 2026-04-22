using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingWebb.Models
{
    public class UserMatch
    {
        [Key]
        public int Id { get; set; }

        // --- CÁC THUỘC TÍNH QUAN TRỌNG ĐỂ FIX LỖI CS1061 ---

        [Required]
        public int User1Id { get; set; }

        // Thuộc tính điều hướng này BẮT BUỘC phải có để Fluent API trong DbContext hoạt động
        [ForeignKey("User1Id")]
        public virtual AppUser? User1 { get; set; }

        [Required]
        public int User2Id { get; set; }

        // Thuộc tính điều hướng này BẮT BUỘC để fix lỗi 'UserMatch does not contain a definition for User2'
        [ForeignKey("User2Id")]
        public virtual AppUser? User2 { get; set; }

        // --- GIỮ NGUYÊN DỮ LIỆU CŨ VÀ CẬP NHẬT TÊN BIẾN ---

        public DateTime MatchedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Status { get; set; } = "Đã tương hợp";

        public string Bio { get; set; } = string.Empty; 
    }
}
