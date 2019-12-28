using System;
using System.Runtime.InteropServices;

// ReSharper disable StringLiteralTypo

namespace EncryptDecryptCore
{
    class Program
    {
        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        private static extern bool SetClipboardData(uint uFormat, IntPtr data);

        [STAThread]
        static void Main(string[] args)
        {
            var crypto = new Crypto();

            Console.WriteLine("(E)ncrypt / (D)ecrypt?");
            var encryptDecrypt = Console.ReadLine();

            Console.WriteLine("Value to crypt:");
            var valueToCrypt = Console.ReadLine();

            Console.WriteLine("Result:");
            var result = encryptDecrypt == "E" ? crypto.Encrypt(valueToCrypt) : crypto.Decrypt(valueToCrypt);
            Console.WriteLine(result);

            OpenClipboard(IntPtr.Zero);
            var ptr = Marshal.StringToHGlobalUni(result);
            SetClipboardData(13, ptr);
            CloseClipboard();
            Marshal.FreeHGlobal(ptr);
            Console.WriteLine("Copied to clipboard");

            Console.ReadLine();
        }
    }
}
