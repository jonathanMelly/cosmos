using System.Collections.Generic;

namespace interpreter
{
    public class CosmosData
    {

        public enum Type
        {
            CsharpCode
        }

        private readonly IDictionary<Type,List<object>> data;

        public CosmosData()
        {
            data = new Dictionary<Type,List<object>>();
        }
        

        public CosmosData Merge(CosmosData newer)
        {
            if (newer != null)
            {
                foreach (var (key, newValue) in newer.data)
                {
                    if (data.ContainsKey(key))
                    {
                        data[key].Add(newValue);
                    }
                    else
                    {
                        var list = new List<object>() {newValue};
                        data[key] = list;
                    }

                }
            }

            return this;
        }

        public static CosmosData WithKeyValue(Type key, object value)
        {
            return new CosmosData {data = {[key] = new List<object>() {value}}};
        }
    }
}