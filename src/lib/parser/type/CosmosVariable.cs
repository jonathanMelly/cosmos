namespace lib.parser.type
{
    /// <summary>
    ///     Immutable variable
    /// </summary>
    public class CosmosVariable
    {

        private CosmosCollection _linkedCollection;
        private CosmosTypedValue _linkedCollectionIndex;

        public CosmosVariable(string name, CosmosTypedValue value)
        {
            this.Name = name;
            this.Value = value;
        }


        public string Name { get; }

        public CosmosTypedValue Value { get; }
        

        public CosmosVariable UpdatedTo(CosmosTypedValue newValue)
        {
            return new CosmosVariable(Name, newValue).WithCollection(_linkedCollection,_linkedCollectionIndex);
        }

        public override string ToString()
        {
            return $"name:{Name},value:{Value}";
        }

        public CosmosVariable WithCollection(CosmosCollection linkedCollection, CosmosTypedValue linkedCollectionIndex)
        {
            _linkedCollection = linkedCollection;
            _linkedCollectionIndex = linkedCollectionIndex;

            return this;
        }

        public bool IsCollection()
        {
            return _linkedCollection != null;
        }

        public CosmosCollection LinkedCollection => _linkedCollection;

        public CosmosTypedValue LinkedIndex => _linkedCollectionIndex;
    }
}