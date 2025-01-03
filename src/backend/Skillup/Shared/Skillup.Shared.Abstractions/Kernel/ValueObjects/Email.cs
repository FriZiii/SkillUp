using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using System.Text.RegularExpressions;

namespace Skillup.Shared.Abstractions.Kernel.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        private static readonly Regex _regex = new(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.Compiled);

        public string Value { get; }
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BadRequestException("Invalid email adress");
            }

            if (value.Length > 100)
            {
                throw new BadRequestException("Invalid email adress");
            }

            value = value.ToLowerInvariant();
            if (!_regex.IsMatch(value))
            {
                throw new BadRequestException("Invalid email adress");
            }

            Value = value;
        }

        public static implicit operator string(Email email) => email.Value;

        public static implicit operator Email(string email) => new(email);

        public bool Equals(Email? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Email)obj);
        }

        public override string ToString() => Value;

        public override int GetHashCode() => Value is not null ? Value.GetHashCode() : 0;
    }
}
