using Skillup.Modules.Courses.Core.Options;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class CourseDetails
    {
        public string Subtitle { get; set; }    //subtitle (ex. c# course with practical exercises)
        public string Description { get; set; }    // long course description (exacly what is in the course)
        // TODO : Add Language public string Language { get; set; } 
        public Uri ThumbnailUrl { get; set; } //minature photo of course
        public CourseLevel Level { get; set; }
        public StringListValueObject ObjectivesSummary { get; set; }  //list of things that you will learn throughout the course (ex. you will learn how to program moblie apps)
        public StringListValueObject MustKnowBefore { get; set; }    //list of things you should already know before starting the course (ex. you should know basics of object-oriented progemming)
        public StringListValueObject IntendedFor { get; set; }    // for whom is this course intended for (ex. for everyone / people the know the basics of programming)

        public CourseDetails()
        {
            Subtitle = string.Empty;
            Description = string.Empty;
            ThumbnailUrl = CourseModuleOptions.DefaultValues.DefaultTubnailPicture;
            Level = CourseLevel.None;
            ObjectivesSummary = new();
            MustKnowBefore = new();
            IntendedFor = new();
        }
    }
}
