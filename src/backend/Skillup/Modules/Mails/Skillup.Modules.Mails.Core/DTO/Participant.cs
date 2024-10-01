using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Mails.Core.DTO
{
    internal class Participant
    {
        public Email Email { get; set; }
        public string? Name { get; set; }
    }
}
