namespace interpreter
{
    /// <summary>
    ///     Immutable variable
    /// </summary>
    public class CosmosVariable
    {
        public CosmosVariable(string name, CosmosTypedValue value)
        {
            this.Name = name;
            this.Value = value;
        }


        public string Name { get; }

        public CosmosTypedValue Value { get; }

        public CosmosVariable UpdatedTo(CosmosTypedValue newValue)
        {
            return new CosmosVariable(Name, newValue);
        }

        public override string ToString()
        {
            return $"name:{Name},value:{Value}";
        }
    }
}