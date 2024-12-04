namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises
{
    public class FillTheGapWord
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public int Index { get; set; }

        public Guid SentenceId { get; set; }
        public FillTheGapSentence Sentence { get; set; }
    }
}
