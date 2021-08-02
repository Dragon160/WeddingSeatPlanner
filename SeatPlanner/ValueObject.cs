using System;
using System.Collections.Generic;
using System.Linq;

namespace SeatPlanner
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public override bool Equals(object obj) =>
            obj is T valueObject &&
            valueObject.GetType() == GetType() &&
            GetEqualityComponents()
                .SequenceEqual(valueObject.GetEqualityComponents());

        public override int GetHashCode() =>
            GetEqualityComponents()
                .Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));

        protected abstract IEnumerable<object> GetEqualityComponents();

        #pragma warning disable S3875 // All subclasses are intended to have value semantics and thus can be compared by == operator.
        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
