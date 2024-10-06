namespace Skillup.Modules.Courses.Core.Entities
{
    public record StringList
    {
        public List<string> Values { get; set; }

        public StringList(List<string> values)
        {
            Values = values ?? throw new ArgumentNullException();
        }
        public StringList Add(string value)
        {
            var newList = new List<string>(Values) { value };
            return new StringList(newList);
        }

        public StringList Remove(string value)
        {
            var newList = new List<string>(Values);
            newList.Remove(value);
            return new StringList(newList);
        }
    }
}
