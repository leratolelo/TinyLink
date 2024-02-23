using System.Security.Cryptography;
using System.Text;

namespace TinyLink
{
    public class HashHelper
    {
        
        private string GenerateHash(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                var  _hashLength = 8;
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").Substring(0, _hashLength);
            }
        }
        public string GenerateHashUrl(string url)
        {
            var uniqueInput = $"{url}{Guid.NewGuid()}";
            return GenerateHash(uniqueInput);
        }

    }
}
