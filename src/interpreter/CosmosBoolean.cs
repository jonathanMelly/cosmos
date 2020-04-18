using interpreter.extensions;

namespace interpreter
{
    public class CosmosBoolean : CosmosTypedValue
    {
        public CosmosBoolean(bool value) : base(value)
        {
        }

        public bool Value => (bool) RawValue;

        public static CosmosBoolean operator !(CosmosBoolean subject)
        {
            return (!subject.Value).AsCosmosBoolean();
        }

        public override CosmosBoolean Boolean()
        {
            return this;
        }

        public override int CompareTo(CosmosTypedValue other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            if (other is CosmosBoolean otherCb)
                return Value.CompareTo(otherCb.Value);

            throw new InvalidComparisonException(
                $"Cannot compare a {GetType()} [{rawValue}] with {other.GetType()} [{other}]");
        }
    }
}