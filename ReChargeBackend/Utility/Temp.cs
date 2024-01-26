using System.Security.Cryptography;

namespace ReChargeBackend.Utility
{
    public static class Temp
    {
        //TODO: this is temporary, remove it later
        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            if (phoneNumber.Length < 10 || (phoneNumber[0] != '7' && phoneNumber[0] != '8'))
            {
                return false;
            }
            return true;
        }
        //TODO: this is temporary, remove it later
        public static string GenerateCode()
        {
            return "12345";
        }
        //TODO: this is temporary, remove it later
        public static string GenerateSessionId()
        {
            return "12345";
        }


        private static Random random = new Random();

        public static string GenerateAccessToken()
        {
            int length = 7;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
        }
    }
}
