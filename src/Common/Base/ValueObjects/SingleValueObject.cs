using System;
using System.Collections.Generic;

namespace AGTec.Common.Base.ValueObjects
{
    public abstract class SingleValueObject<T> : ValueObject, IComparable, ISingleValueObject<T>
        where T : IComparable, IComparable<T>
    {
        private T _value { get; }

        protected SingleValueObject(T value)
        {
            _value = value;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var other = obj as SingleValueObject<T>;
            if (other == null)
            {
                throw new ArgumentException($"Cannot compare '{GetType()}' and '{obj.GetType()}'");
            }

            return _value.CompareTo(other._value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }

        public override string ToString()
        {
            return ReferenceEquals(_value, null)
                ? string.Empty
                : _value.ToString();
        }

        public T GetValue()
        {
            return _value;
        }
    }
}