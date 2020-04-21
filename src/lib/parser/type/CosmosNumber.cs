using System;
using lib.parser.exception;

namespace lib.parser.type
{
    public class CosmosNumber : CosmosTypedValue
    {
        public CosmosNumber(decimal value) : base(value)
        {
        }

        public CosmosNumber(int value) : base(Convert(value))
        {
        }

        public CosmosNumber(float value) : base(Convert(value))
        {
        }

        public CosmosNumber(double value) : base(Convert(value))
        {
        }

        public CosmosNumber(string value) : base(Convert(value))
        {
        }

        public decimal Value => (decimal) RawValue;

        private static decimal Convert(IComparable numberExpression)
        {
            try
            {
                return System.Convert.ToDecimal(numberExpression);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Cannot convert {numberExpression} to number", e);
            }
        }

        public override CosmosNumber Number()
        {
            return this;
        }

        public override int CompareTo(CosmosTypedValue other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            if (other is CosmosNumber otherCn)
                return Value.CompareTo(otherCn.Value);

            throw new InvalidComparisonException(
                $"Cannot compare a {GetType()} [{rawValue}] with {other.GetType()} [{other}]");
        }

        public CosmosNumber Nroot(CosmosNumber n)
        {
            return new CosmosNumber(Math.Pow(System.Convert.ToDouble(Value), System.Convert.ToDouble(n.Value)));
        }

        public static CosmosNumber operator -(CosmosNumber a, CosmosNumber b)
        {
            return new CosmosNumber(a.Value - b.Value);
        }

        public static CosmosNumber operator +(CosmosNumber a, CosmosNumber b)
        {
            return new CosmosNumber(a.Value + b.Value);
        }

        public static CosmosNumber operator *(CosmosNumber a, CosmosNumber b)
        {
            return new CosmosNumber(a.Value * b.Value);
        }

        public static CosmosNumber operator /(CosmosNumber a, CosmosNumber b)
        {
            return new CosmosNumber(a.Value / b.Value);
        }

        public static CosmosNumber operator ^(CosmosNumber a, CosmosNumber b)
        {
            return new CosmosNumber(Math.Pow(System.Convert.ToDouble(a.Value), System.Convert.ToDouble(b.Value)));
        }

        public static CosmosNumber operator -(CosmosNumber a)
        {
            return new CosmosNumber(-a.Value);
        }
    }
}