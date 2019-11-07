using System;
using System.Text;
using System.Security.Cryptography;

namespace Lab_3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int N = SimpleNumber(SimpleNumber(RandomNumber(100000000,999999999)));
            Console.WriteLine(N);




            Console.ReadLine();
        }

        public static string H(string data, string key)
        {
            byte[] data1 = Encoding.ASCII.GetBytes(data);
            byte[] key1 = Encoding.ASCII.GetBytes(key);

            using (var hmac = new HMACSHA1(key1))
            {
                return Encoding.ASCII.GetString(hmac.ComputeHash(data1));
            }
        }

        string Salt(int length)
        {
            RNGCryptoServiceProvider p = new RNGCryptoServiceProvider();
            var salt = new byte[length];
            p.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        static int RandomNumber(int a, int b) 
        {
            Random rand = new Random();
            return rand.Next(a,b);
        }

        static int SimpleNumber(int randnum)
        {
            return randnum * 2 + 1;
        }
    }
}
