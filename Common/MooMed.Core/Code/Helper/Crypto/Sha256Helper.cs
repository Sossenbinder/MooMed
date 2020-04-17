using System;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;
using MooMed.Core.Code.Extensions;

namespace MooMed.Core.Code.Helper.Crypto
{
    public static class Sha256Helper
    {
        [NotNull]
        private static readonly SHA256Managed _sha256Managed;
        
        static Sha256Helper()
        {
            _sha256Managed = new SHA256Managed();
        }

        [NotNull]
        public static string Hash([NotNull] string toHash)
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
