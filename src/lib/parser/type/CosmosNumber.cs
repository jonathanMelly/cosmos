using System;
using System.Globalization;
using lib.parser.exception;

namespace lib.parser.type
{
    public class CosmosNumber : CosmosTypedValue
    {
        //To avoid issues with . and , ... (could be better...)
        private static IFormatProvider format = new CultureInfo("en-US");

        public bool Leading0 { get; set; } = false;

        public CosmosNumber(decimal value,bool leading0=false) : base(value)
        {
            Leading0 = leading0;
        }

        public CosmosNumber(int value,bool leading0=false) : base(Convert(value))
        {
            Leading0 = leading0;
        }

        public CosmosNumber(float value,bool leading0=false) : base(Convert(value))
        {
            Leading0 = leading0;
        }

        public CosmosNumber(double value,bool leading0=false) : base(Convert(value))
        {
            Leading0 = leading0;
        }

        public CosmosNumber(string value,bool leading0=false) : base(Convert(value))
        {
            Leading0 = leading0;
        }

        public decimal Value => (decimal) RawValue;

        private static decimal Convert(IComparable numberExpression)
        {
            try
            {
                return System.Convert.ToDecimal(numberExpression,format);
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

        public override string ToString()
        {
            return $"{(Leading0 && Value<10?"0":"")}{base.ToString()}";
        }
    }
}