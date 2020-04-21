namespace lib.extension
{
    public static class StringExtension
    {
        public static CosmosString AsCosmosString(this string subject)
        {
            return CosmosTypedValue.String(subject);
        }

        public static CosmosVariable AsCosmosVariable(this string subject, CosmosTypedValue value = null)
        {
            return new CosmosVariable(subject, value);
        }
        
    }
}