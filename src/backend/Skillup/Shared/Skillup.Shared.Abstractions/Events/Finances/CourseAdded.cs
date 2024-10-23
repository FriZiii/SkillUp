using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Shared.Abstractions.Events.Finances
{
    public class CourseAdded : IItemAdded
    {
        public Guid ItemId { get; private set; }
        public Guid AuthorId { get; private set; }
        public ItemType Type { get; private set; }


        public CourseAdded(Guid itemId, Guid authorId)
        {
            ItemId = itemId;
            AuthorId = authorId;
            Type = ItemType.Course;
        }
    }
}
