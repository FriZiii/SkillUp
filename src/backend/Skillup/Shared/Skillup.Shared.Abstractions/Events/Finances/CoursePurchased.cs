namespace Skillup.Shared.Abstractions.Events.Finances
{
    public record CoursePurchased(Guid CourseId, Guid UserId, Guid AuthorId);
}
