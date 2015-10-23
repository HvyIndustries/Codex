namespace Codex.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    
    public class Md5
    {
        /// <summary>
        /// Create an MD5 hash for a given string
        /// </summary>
        public static string CreateHash(string input)
        {
            input = input.ToLower();

            var encodedPassword = new UTF8Encoding().GetBytes(input);

            var hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            var encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            return encoded;
        }
    }
}
