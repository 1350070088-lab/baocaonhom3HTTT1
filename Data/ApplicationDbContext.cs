using Microsoft.EntityFrameworkCore;
using DatingWebb.Models;

namespace DatingWebb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Bảng chứa danh sách người dùng chính
        public DbSet<AppUser> AppUsers { get; set; }

        // Bảng quản lý các lượt tương hợp (Like/Match)
        public DbSet<UserMatch> UserMatches { get; set; }

        // Bảng quản lý doanh thu/thanh toán
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Cấu hình bảng AppUsers
            modelBuilder.Entity<AppUser>(entity => {
                entity.ToTable("AppUsers");
                entity.HasKey(e => e.Id);
            });

            // 2. Cấu hình bảng UserMatches và xử lý xung đột khóa ngoại (Foreign Key)
            // Vì AppUser có 2 mối quan hệ với UserMatch (Người gửi và Người nhận), 
            // mày cần chặn việc xóa dây chuyền (Restrict) để tránh lỗi khi Update Database.
            modelBuilder.Entity<UserMatch>(entity => {
                entity.ToTable("UserMatches");
                entity.HasKey(e => e.Id);

                // Cấu hình quan hệ với người gửi (User1)
                entity.HasOne(m => m.User1)
                      .WithMany(u => u.SentMatches)
                      .HasForeignKey(m => m.User1Id)
                      .OnDelete(DeleteBehavior.Restrict); 

                // Cấu hình quan hệ với người nhận (User2)
                entity.HasOne(m => m.User2)
                      .WithMany(u => u.ReceivedMatches)
                      .HasForeignKey(m => m.User2Id)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 3. Cấu hình bảng Payments
            modelBuilder.Entity<Payment>(entity => {
                entity.ToTable("Payments");
                entity.HasOne(p => p.User)
                      .WithMany(u => u.Payments)
                      .HasForeignKey(p => p.UserId);
            });
        }
    }
}
