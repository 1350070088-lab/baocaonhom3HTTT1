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

        // ĐỔI TÊN THÀNH UserMatches để khớp với HomeController của m
        public DbSet<Match> UserMatches { get; set; }

        // Bảng quản lý doanh thu/thanh toán
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình tên bảng trong Database
            modelBuilder.Entity<AppUser>().ToTable("AppUsers");
            modelBuilder.Entity<Match>().ToTable("UserMatches"); // Đồng bộ tên bảng ở đây luôn
            modelBuilder.Entity<Payment>().ToTable("Payments");
        }
    }
}