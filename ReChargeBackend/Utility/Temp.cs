using System.Security.Cryptography;

namespace ReChargeBackend.Utility
{
    public static class Temp
    {
        //TODO: this is temporary, remove it later
        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            //if (phoneNumber.Length < 10 || (phoneNumber[0] != '7' && phoneNumber[0] != '8' && phoneNumber[0] != '+'))
            //{
            //    return false;
            //}
            return true;
        }
        public static string GenerateCode()
        {
            int length = 5;
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
        }
        public static string GenerateSessionId()
        {
            int length = 8;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
        }


        private static Random random = new Random();

        public static string GenerateAccessToken()
        {
            int length = 7;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
        }
        public static string GenerateAccessCode()
        {
            int length = 7;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
        }
    }
}
