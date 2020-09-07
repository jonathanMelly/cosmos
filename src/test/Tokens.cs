namespace test
{
    public static class Tokens
    {
        public const string False = "faux";
        public const string False2 = "KO";
        public const string False3 = "(2!=2)";
        
        public const string True = "vrai";
        public const string True2 = "OK";
        public const string True3 = "(1==1)";
        
        public const string And = "et";
        public const string Or = "ou";
        
        public const string Lpar = "(";
        public const string Rpar = ")";

        public const string Xor = "ou au contraire";
        
        public static readonly string[] Gt = {"est plus grand que ",">"};

        public static readonly string[] Lt = {"est plus petit que ","<"};

        public static readonly string[] IsEqual = {"est égal à ","vaut ","=="};
        
        public static readonly string[] IsDifferent = {"est différent de ","<>","!="};

        public static readonly string[] Gte = {"est plus grand ou égal à ",">="};
        
        public static readonly string[] Lte = {"est plus petit ou égal à ","<="};
    }
}