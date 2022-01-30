using Cryptography.Interfaces;
using System;
using System.Text;

namespace Cryptography.Services
{
    public class CryptographyServices : ICryptographyService
    {
        public bool DecryptAndCheck(string clearText, string salt, string encryptedSecret)
        {
            try
            {
                string encCode = EncryptSecret(salt, clearText);
                string midPhase1 = Decrypt(encryptedSecret ?? "");
                string midPhase2 = Decrypt(encCode);
                string hexsalt = GetHexString(salt ?? "");
                #pragma warning disable CA1307 // Specify StringComparison
                if (String.Compare(midPhase1.Replace(hexsalt, string.Empty), midPhase2.Replace(hexsalt, string.Empty)) == 0)
                #pragma warning restore CA1307 // Specify StringComparison
                    return true;
                return false;//.Substring(10);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }


        public string EncryptSecret(string secretSalt, string secret)
        {
            string returnValue;
            //returnValue = String.Empty;
            if (!string.IsNullOrEmpty(secret))
            {
                try
                {
                    #pragma warning disable CA1062 // Validate arguments of public methods
                    returnValue = Encrypt(secretSalt.ToString(), secret.ToString());
                    #pragma warning restore CA1062 // Validate arguments of public methods
                    return returnValue;
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        throw e.InnerException;

                    throw;
                }
            }
            return string.Empty;

        }


        public string GetSecretSalt()
        {

            string returnValue;
            //returnValue = String.Empty;
            try
            {
                returnValue = GetRandomSecretSalt();
                return returnValue;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }

        }

        #region Private Methods

        private string GetRandomSecretSalt()
        {
            try
            {
                int saltLength = 10;
                string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()_+|";
                string returnValue = string.Empty;

                Random random = new Random();
                StringBuilder sb = new StringBuilder(saltLength);
                for (int i = 0; i < saltLength; i++)
                {
                    int randomNumber = random.Next(0, characters.Length);
                    sb.Append(characters, randomNumber, 1);
                }
                returnValue = sb.ToString();
                return returnValue;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }

        private string Encrypt(string secretSalt, string clearSecret)
        {
            try
            {
                string returnValue = string.Empty;

                secretSalt = GetHexString(secretSalt);
                clearSecret = GetHexString(clearSecret);

                System.Security.Cryptography.HMACMD5 hash = new System.Security.Cryptography.HMACMD5();

                byte[] returnBytes = new byte[secretSalt.Length / 2];
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(secretSalt.Substring(i * 2, 2), 16);
                hash.Key = returnBytes;

                string encodedSecret = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(clearSecret)));

                #pragma warning disable CA1305 // Specify IFormatProvider
                string newSecret = string.Format("{0}{1}", secretSalt, encodedSecret);
                #pragma warning restore CA1305 // Specify IFormatProvider
                byte[] bytes = Encoding.UTF8.GetBytes(newSecret);
                StringBuilder sb = new StringBuilder();
                foreach (byte bt in bytes)
                {
                    #pragma warning disable CA1305 // Specify IFormatProvider
                    sb.AppendFormat("{0:x2}", bt);
                    #pragma warning restore CA1305 // Specify IFormatProvider
                }
                returnValue = sb.ToString();
                return returnValue;

            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }

        private string GetHexString(string input)
        {
            try
            {
                char[] values = input.ToCharArray();
                string hexOutput = string.Empty;
                foreach (char letter in values)
                    #pragma warning disable CA1305 // Specify IFormatProvider
                    hexOutput = String.Concat(hexOutput, String.Format("{0:X}", Convert.ToInt32(letter)));
                    #pragma warning restore CA1305 // Specify IFormatProvider
                return hexOutput;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }

        private string Decrypt(string encryptedSecret)
        {
            try
            {
                string returnValue = string.Empty;

                int NumberChars = encryptedSecret.Length;
                byte[] bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(encryptedSecret.Substring(i, 2), 16);
                }

                return returnValue = System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }


        }

        #endregion
    }
}
