using Skillup.Modules.Finances.Core.Entities;
using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Finances.Core.Exceptions
{
    internal class ItemNotFoundException : SkillupException
    {
        public Guid Item { get; }
        public ItemType Type { get; }

        public ItemNotFoundException(Guid item) : base($"Item with ID: '{item}' was not found.")
        {
            Item = item;
        }

        public ItemNotFoundException(ItemType type) : base($"Item with Type: '{nameof(type)}' was not found.")
        {
            Type = type;
        }
    }
}
