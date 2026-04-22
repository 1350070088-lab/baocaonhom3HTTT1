using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingWebb.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        // Đổi từ AppUserId thành UserId để fix lỗi CS1061
        [Required]
        public int UserId { get; set; }

        // Liên kết với bảng User để lấy thông tin người thanh toán
        [ForeignKey("UserId")]
        public virtual AppUser? User { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Success, Failed, Pending

        public string? TransactionId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
