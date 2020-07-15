using MooMed.DotNet.Extensions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MooMed.Encryption
{
    public static class Sha256Helper
    {
        private static readonly SHA256Managed _sha256Managed;
        
        static Sha256Helper()
        {
            _sha256Managed = new SHA256Managed();
        }

        public static string Hash(string toHash)
        {
            if (toHash.IsNullOrEmpty())
            {
                throw new ArgumentException("Cant hash invalid string");
            }

            var hash = new StringBuilder();
            byte[] crypto = _sha256Managed.ComputeHash(Encoding.ASCII.GetBytes(toHash));

            foreach (var cryptoByte in crypto)
            {
                hash.Append(cryptoByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
