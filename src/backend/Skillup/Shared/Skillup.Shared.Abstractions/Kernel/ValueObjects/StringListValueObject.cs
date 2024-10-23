namespace Skillup.Shared.Abstractions.Kernel.ValueObjects
{
    public record StringListValueObject
    {
        public IEnumerable<string> Values { get; set; }

        public StringListValueObject(List<string> values)
        {
            Values = values ?? throw new ArgumentNullException();
        }

        public StringListValueObject()
        {
            Values = Enumerable.Empty<string>();
        }

        public StringListValueObject Add(string value)
        {
            var newList = new List<string>(Values) { value };
            return new StringListValueObject(newList);
        }

        public StringListValueObject Remove(string value)
        {
            var newList = new List<string>(Values);
            newList.Remove(value);
            return new StringListValueObject(newList);
        }
    }
}
