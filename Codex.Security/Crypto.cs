namespace Codex.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// The crypto.
    /// </summary>
    public class Crypto
    {
        // The following constants may be changed without breaking existing hashes.

        /// <summary>
        /// The salt byte size.
        /// </summary>
        private const int SALT_BYTE_SIZE = 24;

        /// <summary>
        /// The hash byte size.
        /// </summary>
        private const int HASH_BYTE_SIZE = 24;

        /// <summary>
        /// The pbkdf2 iterations.
        /// </summary>
        private const int PBKDF2_ITERATIONS = 1000;

        /// <summary>
        /// The iteration index.
        /// </summary>
        private const int ITERATION_INDEX = 0;

        /// <summary>
        /// The salt index.
        /// </summary>
        private const int SALT_INDEX = 1;

        /// <summary>
        /// The pbkdf2 index.
        /// </summary>
        private const int PBKDF2_INDEX = 2;

        /// <summary>
        /// Creates a salted PBKDF2 hash of the given password.
        /// </summary>
        /// <param name="password">
        /// The password to hash.
        /// </param>
        /// <returns>
        /// The hash of the password.
        /// </returns>
        public static string CreateHash(string password)
        {
            // Generate a random salt
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            var hash = Pbkdf2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return PBKDF2_ITERATIONS + ":" + Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Validates a password given a hash of the correct one.
        /// </summary>
        /// <param name="password">
        /// The password to check.
        /// </param>
        /// <param name="correctHash">
        /// A hash of the correct password.
        /// </param>
        /// <returns>
        /// True if the password is correct. False otherwise.
        /// </returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            if (correctHash == null)
            {
                return false;
            }

            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = int.Parse(split[ITERATION_INDEX]);
            var salt = Convert.FromBase64String(split[SALT_INDEX]);
            var hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            var testHash = Pbkdf2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Generate a 64 char random API Key
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GenerateApiKey()
        {
            const int ByteSize = 0x100;
            const int Length = 64;
            const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var allowedCharSet = new HashSet<char>(AllowedChars).ToArray();

            using (var rng = new RNGCryptoServiceProvider())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < Length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < Length; ++i)
                    {
                        // Divide the byte into allowedCharSet-sized groups. If the
                        // random value falls into the last group and the last group is
                        // too small to choose from the entire allowedCharSet, ignore
                        // the value in order to avoid biasing the result.
                        var outOfRangeStart = ByteSize - (ByteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }

                return result.ToString();
            }
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">
        /// The first byte array.
        /// </param>
        /// <param name="b">
        /// The second byte array.
        /// </param>
        /// <returns>
        /// True if both byte arrays are equal. False otherwise.
        /// </returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }

        /// <summary>
        /// Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">
        /// The password to hash.
        /// </param>
        /// <param name="salt">
        /// The salt.
        /// </param>
        /// <param name="iterations">
        /// The PBKDF2 iteration count.
        /// </param>
        /// <param name="outputBytes">
        /// The length of the hash to generate, in bytes.
        /// </param>
        /// <returns>
        /// A hash of the password.
        /// </returns>
        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) { IterationCount = iterations };
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
