using BCrypt.Net;

namespace Utility
{
    public class Hasher
    {
        public static string Encrypt(string password)
        {
            // Generate a random salt
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password with the salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            // Store both the salt and hashedPassword in the database
            // Typically, you'd store them in separate columns
            string saltAndHash = salt + hashedPassword;

            return saltAndHash;
        }

        public static bool Verify(string enteredPassword, string storedSaltAndHash)
        {
            // Extract the salt and hashedPassword from the storedSaltAndHash
            string salt = storedSaltAndHash.Substring(0, 29); // The salt is the first 29 characters
            string hashedPassword = storedSaltAndHash.Substring(29);

            // Use the extracted salt to hash the entered password
            string hashedEnteredPassword = BCrypt.Net.BCrypt.HashPassword(enteredPassword, salt);

            // Compare the two hashed passwords
            return hashedEnteredPassword == hashedPassword;
        }
    }
}