using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatingWebb.Models
{
    public class UserProfileViewModel
    {
        public string Bio { get; set; } = string.Empty;
        public string? ExistingPhotoUrl { get; set; } // Fix lỗi CS1061/CS0117
        public IFormFile? PhotoFile { get; set; }
    }

    public class SettingsViewModel // Fix lỗi CS0234
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? NewPassword { get; set; }
    }

    public class RegisterViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AdminDashboardViewModel
    {
        public List<AppUser> Users { get; set; } = new List<AppUser>();
        public int TotalUsers { get; set; }
        public int NewMembers { get; set; }
        public int TotalMatches { get; set; }
        public string Revenue { get; set; } = "0đ";
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
