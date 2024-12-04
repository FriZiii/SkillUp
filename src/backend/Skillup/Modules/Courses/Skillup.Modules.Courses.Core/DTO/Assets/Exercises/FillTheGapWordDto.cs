namespace Skillup.Modules.Courses.Core.DTO.Assets.Exercises
{
    public class FillTheGapWordDto
    {
        public Guid Id { get; set; }
        public Guid SentenceId { get; set; }
        public string Value { get; set; }
        public int Index { get; set; }
    }
}
