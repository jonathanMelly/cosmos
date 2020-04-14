namespace interpreter
{
    public class Variable
    {
        private string name;
        private object value;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public object Value
        {
            get => value;
            set => this.value = value;
        }

        public Variable(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"[name:{Name},value:{Value}]";
        }
    }
}