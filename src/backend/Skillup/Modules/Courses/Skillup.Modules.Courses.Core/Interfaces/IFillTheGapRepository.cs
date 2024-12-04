using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IFillTheGapRepository
    {
        Task AddSentence(FillTheGapSentence fillTheGapSentence);
        Task AddWords(List<FillTheGapWord> words);
        Task<IEnumerable<FillTheGapSentence>> GetFillTheGaps(Guid assignmentId);
    }
}
