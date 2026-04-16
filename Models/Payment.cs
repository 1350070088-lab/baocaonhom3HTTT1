using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingWebb.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AppUserId { get; set; }

        // Liên kết với bảng User
        [ForeignKey("AppUserId")]
        public virtual AppUser? User { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; // Success, Failed, Pending

        public string? TransactionId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}