namespace CodeGeneration.Extensions
{
    public static class StringExtensions
    {
        public static string FirstToLower(this string str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str.Substring(1);

            return str;
        }
        
        public static string FirstToUpper(this string str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsLower(str[0]))
                return str.Length == 1 ? char.ToUpper(str[0]).ToString() : char.ToUpper(str[0]) + str.Substring(1);

            return str;
        }
        
        public static string RemoveFirst(this string str)
        {
            return str.Substring(1);
        }
    }
}