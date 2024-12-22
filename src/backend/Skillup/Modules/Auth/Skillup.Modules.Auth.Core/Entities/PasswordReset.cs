using System.Security.Cryptography;
using System.Text;

namespace Skillup.Modules.Auth.Core.Entities
{
    internal class PasswordReset
    {
        public PasswordReset(User user)
        {
            Id = Guid.NewGuid();
            User = user;
            UserId = User.Id;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = CreatedAt.AddMinutes(15);
            IsActive = true;
            Token = GenerateToken();
        }

        private PasswordReset() { } // for ef core

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }

        public User User { get; set; }

        private string GenerateToken()
        {
            var data = $"{UserId}-{Id}-{CreatedAt.Ticks}-{ExpiresAt.Ticks}";
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hashBytes);
        }
    }
}
