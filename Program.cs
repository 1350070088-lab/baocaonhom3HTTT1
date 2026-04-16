using Microsoft.EntityFrameworkCore;
using DatingWebb.Data;
using DatingWebb.Models; 

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- PHẦN SEED DATA: TỰ ĐỘNG TẠO DỮ LIỆU MẪU ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    // Tự động tạo file database .db nếu chưa tồn tại
    context.Database.EnsureCreated();

    // BƯỚC 1: SEED APPUSERS (Thêm ImageUrl để fix lỗi trang Discover)
    if (!context.AppUsers.Any())
    {
        context.AppUsers.AddRange(
            new AppUser { FullName = "Sarah Thorne", Email = "sarah@example.com", Password = "123", ImageUrl = "https://i.pravatar.cc/600?img=1", CreatedAt = DateTime.Now },
            new AppUser { FullName = "Julianne", Email = "juli@example.com", Password = "123", ImageUrl = "https://i.pravatar.cc/600?img=2", CreatedAt = DateTime.Now },
            new AppUser { FullName = "Emily Stone", Email = "emily@example.com", Password = "123", ImageUrl = "https://i.pravatar.cc/600?img=3", CreatedAt = DateTime.Now },
            new AppUser { FullName = "Chloe", Email = "chloe@example.com", Password = "123", ImageUrl = "https://i.pravatar.cc/600?img=4", CreatedAt = DateTime.Now },
            new AppUser { FullName = "Taehyung", Email = "v@example.com", Password = "123", ImageUrl = "https://i.pravatar.cc/600?img=5", CreatedAt = DateTime.Now }
        );
        context.SaveChanges(); 
    }

    // BƯỚC 2: SEED USERMATCHES
    if (!context.UserMatches.Any())
    {
        var userIds = context.AppUsers.Select(u => u.Id).ToList();

        if (userIds.Count >= 2)
        {
            context.UserMatches.AddRange(
                new Match { User1Id = userIds[0], User2Id = userIds[1], MatchedAt = DateTime.Now, IsActive = true },
                new Match { User1Id = userIds[0], User2Id = userIds[2], MatchedAt = DateTime.Now, IsActive = true }
            );
            context.SaveChanges();
        }
    }
}

// Cấu hình Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();