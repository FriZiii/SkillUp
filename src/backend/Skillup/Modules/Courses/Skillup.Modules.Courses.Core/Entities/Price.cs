using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Core.Entities
{
    public record Price
    {
        public decimal Value { get; }

        public Price(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException();
            }
            Value = value;
        }
    }
}
