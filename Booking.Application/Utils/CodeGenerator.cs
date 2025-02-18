namespace Booking.Application.Utils
{
    public static class CodeGenerator
    {
        private const string Chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        public static string GenerateCode(int length = 6)
        {
            return new string(Enumerable.Repeat(Chars, length)
                .Select(s => s[Random.Shared.Next(s.Length)])
                .ToArray());
        }
    }
} 