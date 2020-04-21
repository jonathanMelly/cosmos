using System;

namespace lib
{
    public class CosmosString : CosmosTypedValue
    {
        public CosmosString(string value) : base(value)
        {
        }

        public string Value => (string) RawValue;

        public override bool IsString => true;

        public override CosmosString String()
        {
            return this;
        }

        public override int CompareTo(CosmosTypedValue other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            if (other is CosmosString otherCs)
                return string.Compare(Value, otherCs.Value, StringComparison.Ordinal);

            throw new InvalidComparisonException(
                $"Cannot compare a {GetType()} [{rawValue}] with {other.GetType()} [{other}]");
        }
    }
}