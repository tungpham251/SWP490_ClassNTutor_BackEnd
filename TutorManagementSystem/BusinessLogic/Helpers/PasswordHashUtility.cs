namespace BusinessLogic.Helpers
{
    public static class PasswordHashUtility
    {
        private const int WorkFactor = 12; 

        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(WorkFactor);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }


    public class PasswordGenerator
    {
        private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NumericChars = "0123456789";
        private const string SpecialChars = "!@#$%^&*()-_+=<>?";

        public static string GenerateRandomPassword(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Password length must be greater than zero.", nameof(length));
            }

            var allChars = LowercaseChars + UppercaseChars + NumericChars + SpecialChars;

            var random = new Random();
            var passwordChars = Enumerable.Range(0, length)
                                           .Select(_ => allChars[random.Next(allChars.Length)])
                                           .ToArray();

            var shuffledPassword = new string(passwordChars.OrderBy(_ => random.Next()).ToArray());

            return shuffledPassword;
        }
    }
}
