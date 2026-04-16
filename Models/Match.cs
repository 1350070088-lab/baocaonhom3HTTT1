using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingWebb.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        // ID của người thứ nhất
        [Required]
        public int User1Id { get; set; }

        // ID của người thứ hai
        [Required]
        public int User2Id { get; set; }

        // Thời điểm hai người "quẹt phải" trúng nhau
        public DateTime MatchedAt { get; set; } = DateTime.Now;

        // Trạng thái (Ví dụ: true là vẫn đang tìm hiểu, false là đã hủy tương hợp)
        public bool IsActive { get; set; } = true;

        // Thiết lập quan hệ để dễ truy vấn (Optional)
        [ForeignKey("User1Id")]
        public virtual AppUser? User1 { get; set; }

        [ForeignKey("User2Id")]
        public virtual AppUser? User2 { get; set; }
    }
}