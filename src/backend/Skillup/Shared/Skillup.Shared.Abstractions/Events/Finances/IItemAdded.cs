using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Shared.Abstractions.Events.Finances
{
    public interface IItemAdded
    {
        Guid ItemId { get; }
        Guid AuthorId { get; }
        ItemType Type { get; }
    }
}
