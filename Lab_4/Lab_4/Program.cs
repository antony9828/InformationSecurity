using System;
using System.Linq;

namespace Lab_4
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int p = SimpleNumber(RandomNumber(1, 10));
            int q = SimpleNumber(RandomNumber(1, 10));
            int n = p * q;
            int s = (p - 1) * (q - 1);
            int d = (p - 1) * (q - 1) - 1;

            int e = 1;

            while ((e ^ d) % s != 1)
            {
                e++;
            }

            Console.WriteLine("P = " + p);
            Console.WriteLine("Q = " + q);
            Console.WriteLine("N = " + n);
            Console.WriteLine("S = " + s);
            Console.WriteLine("Public Key");
            Console.WriteLine("D = " + d);
            Console.WriteLine("Private Key");
            Console.WriteLine("E = " + e);



            string message = "";

            Console.WriteLine("Enter message to encrypt");
            message = Console.ReadLine();

            int[] array = new int[message.Length];
            int[] arrayf = new int[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                Console.WriteLine(message[i] + " = " + (Convert.ToInt32(message[i] - 96)));
                array[i] = ((Convert.ToInt32(message[i]) - 96) ^ e) % 25;
            }

            string encryptedMessage = "";

            for (int i = 0; i < message.Length; i++)
            {
                Console.WriteLine(array[i]);
            }

            for (int i = 0; i < message.Length; i++)
            {
                encryptedMessage += Convert.ToChar(array[i] + 96);
            }

            Console.WriteLine(encryptedMessage);

            Console.WriteLine("Decrypt?");
            Console.ReadKey();

            for (int i = 0; i < message.Length; i++)
            {
                arrayf[i] = ((array[i]) ^ d) % 25;
            }

            string decryptedMessage = "";

            for (int i = 0; i < message.Length; i++)
            {
                decryptedMessage += Convert.ToChar(arrayf[i] + 96);
            }

            Console.WriteLine(decryptedMessage);

            Console.ReadKey();
        }

        static int RandomNumber(int a, int b)
        {
            Random rand = new Random();
            return rand.Next(a, b);
        }

        static int SimpleNumber(int randnum)
        {
            return randnum * 2 + 1;
        }
    }
}
