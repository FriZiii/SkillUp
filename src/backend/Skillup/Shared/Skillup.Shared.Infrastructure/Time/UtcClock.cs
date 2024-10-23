using Skillup.Shared.Abstractions.Time;

namespace Skillup.Shared.Infrastructure.Time
{
    public class UtcClock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
    }
}
