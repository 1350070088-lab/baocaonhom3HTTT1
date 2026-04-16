namespace DatingWebb.Models
{
    // Model cho trang Đăng ký
    public class RegisterViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // Model cho trang Hồ sơ (Profile)
    public class UserProfileViewModel
    {
        public string Bio { get; set; } = string.Empty;
    }

    // Model cho trang Cài đặt (Settings)
    public class SettingsViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    // Model cho trang Báo lỗi (Error)
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}