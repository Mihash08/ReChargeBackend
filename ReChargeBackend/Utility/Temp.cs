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
    }
}
