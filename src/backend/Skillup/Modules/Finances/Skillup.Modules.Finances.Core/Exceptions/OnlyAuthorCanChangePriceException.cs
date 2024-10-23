using Skillup.Modules.Finances.Core.Entities;
using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Finances.Core.Exceptions
{
    internal class OnlyAuthorCanChangePriceException(ItemType type) : SkillupException($"Only author can change {nameof(type)} price.")
    {
    }
}
