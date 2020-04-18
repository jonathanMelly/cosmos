namespace interpreter.extensions
{
    public static class BooleanExtension
    {
        public static CosmosBoolean AsCosmosBoolean(this bool subject)
        {
            return CosmosTypedValue.Boolean(subject);
        }
    }
}