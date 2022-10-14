using System.Collections.Generic;
using lib.parser.exception;

namespace lib.parser.type
{
    public class CosmosCollection : CosmosTypedValue
    {
        public CosmosCollection() : base(new Dictionary<CosmosTypedValue,CosmosTypedValue>())
        {

        }
        
        public Dictionary<CosmosTypedValue,CosmosTypedValue> Value => (Dictionary<CosmosTypedValue,CosmosTypedValue>) RawValue;

        public override CosmosCollection Collection()
        {
            return this;
        }

        public override int CompareTo(CosmosTypedValue other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return other switch
            {
                null => 1,
                CosmosCollection otherCs => other.CompareTo(otherCs),
                _ => throw new InvalidComparisonException(
                    $"Cannot compare a {GetType()} [{rawValue}] with {other.GetType()} [{other}]")
            };
        }
    }
}