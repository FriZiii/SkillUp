namespace Skillup.Modules.Courses.Core.Entities
{
    public class CourseDetails
    {
        public string Description { get; set; }    // long course description (exacly what is in the course)
        public CourseLevel Level { get; set; }     // level of difficulty (advanced, Beginner, Intermediate)
        public StringListValueObject ObjectivesSummary { get; set; }   //list of things that you will learn throughout the course (ex. you will learn how to program moblie apps)
        public StringListValueObject MustKnowBefore { get; set; }      //list of things you should already know before starting the course (ex. you should know basics of object-oriented progemming)
        public StringListValueObject IntendedFor { get; set; }         // for whom is this course intended for (ex. for everyone / people the know the basics of programming)
    }
}
