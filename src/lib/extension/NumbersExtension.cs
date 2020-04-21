using lib.parser.type;

namespace lib.extension
{
    public static class NumbersExtension
    {
        public static CosmosNumber AsCosmosNumber(this int subject)
        {
            return CosmosTypedValue.Number(subject);
        }

        public static CosmosNumber AsCosmosNumber(this double subject)
        {
            return CosmosTypedValue.Number(subject);
        }

        public static CosmosNumber AsCosmosNumber(this float subject)
        {
            return CosmosTypedValue.Number(subject);
        }

        public static CosmosNumber AsCosmosNumber(this decimal subject)
        {
            return CosmosTypedValue.Number(subject);
        }
    }
}