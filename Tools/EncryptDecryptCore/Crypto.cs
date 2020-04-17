using System;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using MooMed.Core.Code.Helper.Crypto;

namespace EncryptDecryptCore
{
    public class Crypto
    {
        private readonly AESHelper _aesHelper;

        public Crypto()
        {
            var encryptedIv = "IfY8XLg/SiXOXR0vWMBaZb6SPaaFDD3FdPnlgZ6AX4FOE9mqwQfprwYReJ2nEEvJokuPI+sC0mmGDeqpR+HOFQRzycY2wOLpAtSnVqLxrCizxqS7hnJ2naHUDeiXOwwR0A+R0jJbBu4eEDaRMaImj2r1n+HBWZz25/3klNYVsFfFxUcjloHGX1P3snELPjyGrqfGYvpdKqOoAfI3nqCkN2VnbAVYW2pXCvqL1KcvS8L6dbaGbxlE39JjdPIyLQ5ZRilQs4CZQRyhjxWl2BL+AKc3ziaVMFzdBQPAwTSiPwN9VSIWGKBPKi6guHwn2KgnFVwaIxWArXTk/bjLCmobFm4lOHU0qP+aNJR6GzQwBGK62/Q8WLBFYQ+9PYqiMzPhZmDrzHjeXZD+ECNdk5LK+8y3c0KmTUG1gGLjEXkuQ9r9D8am/qCsqhc50H+2owCnJVmZs5QTailkKyrtQjSEh34y5fHGV3RaAYmMC8F8vR5l7gexGs0aChQJMxyYdFZ8Kwhf/zuyOzD/vQ84dAmFf0Xya36mAIoHOiaJlpg2avqe+u3L+P98MBeQzXX/LAL10QxnMaIBas3iJOuebb1opADkeCxGAmPuj/oWIGbbJfv320UEmniKez6HNSklZBGYUDRaCowFH1tXZ01idE863gZ0f/mhhFhB9+WWtZ5s8uw=";
            var encryptedKey = "M5ouyc/+l1kTcpowC2APV2pWadQgUxEkV0KT+VmElsdNttvgJ1vVX/7tGAQA+5sIuFLHLnesoagP/UoyD9moZYxIWD2q/RR2AT4+0uZ8xD4dc3F9Tr5F3+B14lfDfLUIp2D9ewvDLti9I/zWoXQHtPlRAs3k2QDQTWmHRbN1uROG0p0KuZBhy5ilyadeOTP/Jl4EQl318y2NKO2E1/5Dv+M8IIb8daiM/g5M0HMJ/4Ff2p8EBVSjqAENwmysZF9KbhtMniU3MevRxXm/9WetTCVMpsfAq5bFRD1yJ24FCAt/zH+RMIt0m3GZMjuKfqH8Qh3FXFqhSgYcRjzPL9HmeP8RLdsG20nm8VRmfxLYg3/4qoUxCiEyCoVw3EQ9utWkAGxF34Cxq8AwXfavB8Oh8HtHkDZ/HKMQ9rUmJOn0YZ3EhVWP3x8h2lBAIZLLKhv6K29wA1JXcyH9qm2IrlSrY7teNI1yRdJXjQGmr6NsOD1CTY4/FrsUEq8RUkh9mzFgkOqF3R5hL18fXrFwhdXmbD5FYwxGdm9AhgQJZNlFPOi6WObiwFkq6lHERFDg5KlsEKNTZ5K+uCwvCGwSRy35OHNdz5902mpaVciv07cU0Tvso4ayG0eRjJR9XEJm6LlgeJLivUROOVLjAJOECTWIvKUBSHV+vIYEe+T/zL9K3dg=";

            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);

            store.Open(OpenFlags.OpenExistingOnly);

            foreach (var cert in store.Certificates)
            {
                if (!cert.Issuer.ToLower().Contains("moomed"))
                {
                    continue;
                }

                var decryptedIv = RSAHelper.DecryptWithCert(cert, Convert.FromBase64String(encryptedIv));
                var decryptedKey = RSAHelper.DecryptWithCert(cert, Convert.FromBase64String(encryptedKey));

                _aesHelper = new AESHelper(decryptedIv, decryptedKey);
            }
        }

        [NotNull]
        public string Encrypt([NotNull] string toEncrypt)
		{
			toEncrypt = toEncrypt.Replace('-', '+').Replace('_', '/').PadRight(4 * ((toEncrypt.Length + 3) / 4), '=');
			var encrypted = _aesHelper.Encrypt(Convert.FromBase64String(toEncrypt));

            return Convert.ToBase64String(encrypted);
        }

        [NotNull]
        public string Decrypt([NotNull] string toDecrypt)
        {
            var decrypted = _aesHelper.Decrypt(Convert.FromBase64String(toDecrypt));

            return Convert.ToBase64String(decrypted);
        }
    }
}
