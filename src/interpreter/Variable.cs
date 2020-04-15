using System;

namespace interpreter
{
    public class Variable
    {
        private readonly string name;
        private object value=null;

        public string Name
        {
            get => name;
        }

        public object Value
        {
            get => value;
            set => this.value = value;
        }

        public Variable(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return $"[name:{Name},value:{Value}]";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Variable) obj);
        }

        protected bool Equals(Variable other)
        {
            return name == other.name && Equals(value, other.value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name);
        }

        public static bool operator ==(Variable left, Variable right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Variable left, Variable right)
        {
            return !Equals(left, right);
        }
    }
}