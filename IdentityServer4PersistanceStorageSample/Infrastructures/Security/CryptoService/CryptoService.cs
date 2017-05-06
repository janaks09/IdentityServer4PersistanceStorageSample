using System.Collections.Generic;
using System.Security.Cryptography;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.Security.CryptoService
{
    public static class CryptoService
    {
        // 24 = 192 bits
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;

        //Generate random Salt Key
        public static byte[] GenerateSalt(int saltByteSize = SaltByteSize)
        {
            using (var saltGenerator = RandomNumberGenerator.Create())
            {
                var salt = new byte[saltByteSize];
                saltGenerator.GetBytes(salt);
                return salt;
            }
        }

        //Generate hash of password
        public static byte[] ComputeHash(string password, byte[] salt,
            int iterations = HasingIterationsCount, int hashByteSize = HashByteSize)
        {
            using (var hashGenerator = new Rfc2898DeriveBytes(password, salt))
            {
                hashGenerator.IterationCount = iterations;
                return hashGenerator.GetBytes(hashByteSize);
            }
        }

        public static bool VerifyHash(string content, byte[] salt, byte[] hash)
        {
            var computedHash = ComputeHash(content, salt);
            return AreHashesEqual(computedHash, hash);
        }


        //Length constant verification - prevents timing attack
        private static bool AreHashesEqual(IReadOnlyList<byte> firstHash, IReadOnlyList<byte> secondHash)
        {
            var minHashLenght = firstHash.Count <= secondHash.Count ? firstHash.Count : secondHash.Count;
            var xor = firstHash.Count ^ secondHash.Count;
            for (var i = 0; i < minHashLenght; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}
