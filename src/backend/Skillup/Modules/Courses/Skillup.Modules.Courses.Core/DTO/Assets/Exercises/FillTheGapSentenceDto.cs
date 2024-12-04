namespace Skillup.Modules.Courses.Core.DTO.Assets.Exercises
{
    public class FillTheGapSentenceDto : ExerciseDto
    {
        public string Sentence { get; set; }
        public IEnumerable<FillTheGapWordDto> Words { get; set; }
    }
}
