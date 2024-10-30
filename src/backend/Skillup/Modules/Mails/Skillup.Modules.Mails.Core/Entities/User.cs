using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Mails.Core.Entities
{
    internal class User
    {
        public Guid Id { get; set; }
        public required Email Email { get; set; }
        public bool AllowMarketingEmails { get; set; }
    }
}
