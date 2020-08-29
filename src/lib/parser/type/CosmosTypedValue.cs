using System;
using System.Collections.Generic;
using System.Diagnostics;
using lib.parser.exception;

namespace lib.parser.type
{
    public abstract class CosmosTypedValue : IComparable<CosmosTypedValue>, IComparable
    {
        protected readonly object rawValue;

        protected CosmosTypedValue(object value)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            rawValue = value;
        }

        private string ExceptionBaseMessage => $"{RawValue} [{RawValue.GetType()}] n'est pas";

        public virtual bool IsString => false;

        protected object RawValue => rawValue;

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is CosmosTypedValue other
                ? CompareTo(other)
                : throw new ArgumentException($"L'argument doit etre de type {nameof(CosmosTypedValue)}");
        }

        public abstract int CompareTo(CosmosTypedValue other);

        //Factories
        public static CosmosBoolean Boolean(bool value)
        {
            return new CosmosBoolean(value);
        }

        public static CosmosString String(string value)
        {
            return new CosmosString(value);
        }

        public static CosmosNumber Number(int value,bool leading0=false)
        {
            return new CosmosNumber(value,leading0);
        }

        public static CosmosNumber Number(float value,bool leading0=false)
        {
            return new CosmosNumber(value,leading0);
        }

        public static CosmosNumber Number(double value,bool leading0=false)
        {
            return new CosmosNumber(value,leading0);
        }

        public static CosmosNumber Number(decimal value,bool leading0=false)
        {
            return new CosmosNumber(value,leading0);
        }

        //Runtime checked getters
        public virtual CosmosBoolean Boolean()
        {
            throw new WrongTypeException($"{ExceptionBaseMessage} {typeof(CosmosBoolean)}");
        }

        public virtual CosmosNumber Number()
        {
            throw new WrongTypeException($"{ExceptionBaseMessage} {typeof(CosmosNumber)}");
        }

        public virtual CosmosString String()
        {
            throw new WrongTypeException($"{ExceptionBaseMessage} {typeof(CosmosString)}");
        }

        public override string ToString()
        {
            return rawValue.ToString();
        }

        protected bool Equals(CosmosTypedValue other)
        {
            return Equals(rawValue, other.rawValue);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CosmosTypedValue) obj);
        }

        public override int GetHashCode()
        {
            return rawValue != null ? rawValue.GetHashCode() : 0;
        }

        public static bool operator ==(CosmosTypedValue left, CosmosTypedValue right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CosmosTypedValue left, CosmosTypedValue right)
        {
            return !Equals(left, right);
        }

        public static bool operator <(CosmosTypedValue left, CosmosTypedValue right)
        {
            return Comparer<CosmosTypedValue>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(CosmosTypedValue left, CosmosTypedValue right)
        {
            return Comparer<CosmosTypedValue>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(CosmosTypedValue left, CosmosTypedValue right)
        {
            return Comparer<CosmosTypedValue>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(CosmosTypedValue left, CosmosTypedValue right)
        {
            return Comparer<CosmosTypedValue>.Default.Compare(left, right) >= 0;
        }
    }
}