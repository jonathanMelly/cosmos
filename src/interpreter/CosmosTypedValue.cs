using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace interpreter
{
    public abstract class CosmosTypedValue : IComparable<CosmosTypedValue>, IComparable
    {
        protected readonly object rawValue;

        protected CosmosTypedValue(object value)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            rawValue = value;
        }

        private string ExceptionBaseMessage => $"{RawValue} [{RawValue.GetType()}] is not";

        public virtual bool IsString => false;

        protected object RawValue => rawValue;

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is CosmosTypedValue other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(CosmosTypedValue)}");
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

        public static CosmosNumber Number(int value)
        {
            return new CosmosNumber(value);
        }

        public static CosmosNumber Number(float value)
        {
            return new CosmosNumber(value);
        }

        public static CosmosNumber Number(double value)
        {
            return new CosmosNumber(value);
        }

        public static CosmosNumber Number(decimal value)
        {
            return new CosmosNumber(value);
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


        /*
        protected bool Equals(CosmosTypedValue other)
        {
            return Equals(rawValue, other.rawValue);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CosmosTypedValue) obj);
        }

        public override int GetHashCode()
        {
            return (rawValue != null ? rawValue.GetHashCode() : 0);
        }

        public static bool operator ==(CosmosTypedValue a, CosmosTypedValue b)
        {
            if (a == null)
            {
                return b == null;
            }

            if (b == null)
            {
                return false;
            }
            return a.rawValue == b.rawValue;
        }
        
        public static bool operator !=(CosmosTypedValue a, CosmosTypedValue b)
        {
            if (a == null)
            {
                    
            }
            
            if (a?.rawValue == null)
            {
                return b?.rawValue != null;
            }

            if (b?.rawValue == null)
            {
                return true;
            }
            return a.rawValue != b.rawValue;
        }
        
        public static bool operator >(CosmosTypedValue a, CosmosTypedValue b)
        {
            if (a == null)
            {
                return false;
            }

            if (b == null)
            {
                return true;
            }

            if (a.rawValue is IComparable ac && b.rawValue is IComparable bc)
            {
                return ac.CompareTo(bc)>0;
            }
            
            throw new InvalidComparisonException($"Cannot compare {a} with {b} for operator >");
            
        }
        
        
        public static bool operator <(CosmosTypedValue a, CosmosTypedValue b)
        {
            if (a == null)
            {
                return false;
            }

            if (b == null)
            {
                return false;
            }
            
            if (a.rawValue is IComparable ac && b.rawValue is IComparable bc)
            {
                return ac.CompareTo(bc)<0;
            }
            
            throw new InvalidComparisonException($"Cannot compare {a} with {b} for operator <");
        }
        
        public static bool operator <=(CosmosTypedValue a, CosmosTypedValue b)
        {
            if (a == null)
            {
                return b==null;
            }

            if (b == null)
            {
                return false;
            }
            
            if (a.rawValue is IComparable ac && b.rawValue is IComparable bc)
            {
                return ac.CompareTo(bc)<=0;
            }
            
            throw new InvalidComparisonException($"Cannot compare {a} with {b} for operator <=");
        }
        
        public static bool operator >=(CosmosTypedValue a, CosmosTypedValue b)
        {
            if (a == null)
            {
                return b==null;
            }

            if (b == null)
            {
                return true;
            }
            
            if (a.rawValue is IComparable ac && b.rawValue is IComparable bc)
            {
                return ac.CompareTo(bc)>=0;
            }
            
            throw new InvalidComparisonException($"Cannot compare {a} with {b} for operator >=");
        }
        */
    }
}