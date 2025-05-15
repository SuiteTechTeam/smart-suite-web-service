using SweetManagerIotWebService.API.IAM.Application.Internal.OutboundServices;
using System.Security.Cryptography;
using System.Text;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Hashing.Argon2Id.Services
{
    public class HashingService : IHashingService
    {
        public string CreateSalt()
        {
            var buffer = new byte[16];

            using (var rng = RandomNumberGenerator
                       .Create()) rng.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        public string HashCode(string code, string salt)
        {
            Konscious.Security
                .Cryptography.Argon2id encryptionCode =
                    new(Encoding.UTF8.GetBytes(code))
                    {
                        Salt = Encoding.UTF8.GetBytes(salt),
                        DegreeOfParallelism = 8,
                        Iterations = 4,
                        MemorySize = 1024 * 1024
                    };

            return Convert.ToBase64String
                (encryptionCode.GetBytes(16));
        }

        public bool VerifyHash(string code, string salt, string hash)
        {
            string newHash = HashCode(code, salt);

            return hash.SequenceEqual(newHash);
        }
    }
}
