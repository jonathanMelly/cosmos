namespace test.extension
{
    
    public static class StringExtension
    {
        private static string Append(string subject,string toAppend)
        {
            return $"{subject} {toAppend} ";
        }
        
        public static string And(this string subject)
        {
            return Append(subject, Tokens.And);
        }
        
        public static string Or(this string subject)
        {
            return Append(subject, Tokens.Or);
        }
        
        public static string Xor(this string subject)
        {
            return Append(subject, Tokens.Xor);
        }
        
        public static string Gt(this string subject,string tocompare,int variant=0)
        {
            return Append(subject, Tokens.Gt[variant]+" "+tocompare);
        }
        
        public static string Lt(this string subject,string toCompare,int variant=0)
        {
            return Append(subject, Tokens.Lt[variant]+" "+toCompare);
        }
        
        public static string Gte(this string subject,string toCompare,int variant=0)
        {
            return Append(subject, Tokens.Gte[variant]+" "+toCompare);
        }
        
        public static string Lte(this string subject,string toCompare,int variant=0)
        {
            return Append(subject, Tokens.Lte[variant]+" "+toCompare);
        }
        
        public static string IsEqualTo(this string subject,int variant=0)
        {
            return Append(subject, Tokens.IsEqual[variant]);
        }
        
        public static string IsDifferentThan(this string subject,string toCompare =null,int variant=0)
        {
            return Append(subject, Tokens.IsDifferent[variant]+" "+(toCompare??""));
        }
        
        public static string True(this string subject,string append=null)
        {
            return Append(subject, Tokens.True)+" "+(append??"");
        }
        
        public static string True2(this string subject)
        {
            return Append(subject, Tokens.True2);
        }
        
        public static string True3(this string subject)
        {
            return Append(subject, Tokens.True3);
        }
        
        public static string False(this string subject)
        {
            return Append(subject, Tokens.False);
        }
        
        public static string False2(this string subject)
        {
            return Append(subject, Tokens.False2);
        }
        
        public static string False3(this string subject)
        {
            return Append(subject, Tokens.False3);
        }


        public static string Group(this string subject, string inside)
        {
            return $"{subject} {Tokens.Lpar}{inside}{Tokens.Rpar}";
        }

    }
    
}