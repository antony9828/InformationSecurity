using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Numerics;

namespace Lab_3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int ii = 10;
            Console.WriteLine(ii.GetHashCode());

            List<BigInteger> primes = new List<BigInteger>();
            for (int i = 0; i < 100; i++)
            {
                if (IsPrime(i))
                {
                    primes.Add(i);
                    //Console.WriteLine(i);
                }
            }
            Random rand = new Random();

            BigInteger q = primes[rand.Next(0, primes.Count - 1)];
            Console.WriteLine("q = " + q);

            BigInteger N = 2 * q + 1;
            Console.WriteLine("n = " + N);

            List<BigInteger> gList = new List<BigInteger>();

            for (int i = 2; i < N - 2; i++)
            {
                gList.Add(i);
            }

            BigInteger g = gList[rand.Next(0, gList.Count - 1)];

            BigInteger k = 3;

            Console.WriteLine("Enter password: ");
            BigInteger password = Convert.ToInt64(Console.ReadLine()).GetHashCode();
            Console.WriteLine("password: " + password);

            BigInteger salt = rand.Next(100, 999);
            Console.WriteLine("salt: " + salt);

            int x = (password + salt).GetHashCode();
            Console.WriteLine("x: " + x);

            BigInteger verifier = BigInteger.Pow(g, x) % N;

            BigInteger X = BigInteger.Pow(g, x) % N;
            BigInteger validX = 0;
            if (true)
                validX = X;

            string user = "username";
            int a = rand.Next(100, 999);
            BigInteger fullA = BigInteger.Pow(g, a) % N;

            List<BigInteger> serverAnswer = new List<BigInteger>();
            bool clientAnswer = false;
            BigInteger fullB = 0;
            int b = 0;

            if (fullA != 0)
            {
                Console.WriteLine("Проверка на сервере прошла!");
                b = rand.Next(100, 999);
                fullB = (k + verifier + BigInteger.Pow(g, b) % N) % N;
                serverAnswer.Add(salt);
                serverAnswer.Add(fullB);
            }
            else
            {
                Console.WriteLine("Проверка на сервере не прошла!");
            }

            if (serverAnswer[1] != 0)
                clientAnswer = true;

            BigInteger serverScrambler = (fullA + serverAnswer[1]);
            object serverScramblerObject = serverScrambler;
            BigInteger serverScramblerBig = (BigInteger)serverScramblerObject;
            int serverScramblerInt = (int)serverScramblerBig;


            BigInteger clientScrambler = (fullA + serverAnswer[1]);
            object clientScramblerObject = clientScrambler;
            BigInteger clientScramblerBig = (BigInteger)clientScramblerObject;
            int clientScramblerInt = (int)clientScramblerBig;

            if (serverScrambler != clientScrambler && clientAnswer)
                Console.WriteLine("Соединение с сервером прервано!");
            else
                Console.WriteLine("Соединение с сервером установлено!");

            int newX = (password + salt).GetHashCode();

            BigInteger sessionKeyClient = (BigInteger.Pow((fullB - k * (BigInteger.Pow(g,newX) % N)),(a + (clientScramblerInt * newX)))) % N;
            BigInteger sessionKeyServer = (BigInteger.Pow((fullA * (BigInteger.Pow(verifier, serverScramblerInt) % N)), b)) % N;

            BigInteger SessionKeyHashClient = sessionKeyClient.GetHashCode();
            BigInteger SessionKeyHashServer = sessionKeyServer.GetHashCode();

            Console.WriteLine(SessionKeyHashClient);
            Console.WriteLine(SessionKeyHashServer);

            if (SessionKeyHashClient == SessionKeyHashServer)
                Console.WriteLine("Проверка прошла успешно (1)");
            else
                Console.WriteLine("Проверка прервалась (1)");

            int hashForM = N.GetHashCode() ^ g.GetHashCode();

            BigInteger matchClient = (hashForM + user.GetHashCode() + salt + fullA + fullB + SessionKeyHashClient).GetHashCode();
            BigInteger matchServer = (hashForM + user.GetHashCode() + salt + fullA + fullB + SessionKeyHashServer).GetHashCode();

            if (matchClient == matchServer)
            {
                Console.WriteLine("Проверка прошла успешно (2)");
                BigInteger returnClient = (fullA + matchClient + SessionKeyHashClient);
                BigInteger returnServer = (fullA + matchServer + SessionKeyHashServer);
                if (returnClient == returnServer)
                    Console.WriteLine("Авторизация прошла успешно");
                else
                    Console.WriteLine("Авторизация прервалась");
            }
            else
                Console.WriteLine("Проверка прервалась (2)");


            /*
            BigInteger N = SimpleNumber((RandomNumber(100,999)));
            Console.WriteLine("N = " + N);
            BigInteger cycleNumber = 3; 
        AGAIN:
            BigInteger g = SimpleNumber((cycleNumber));
            cycleNumber = g;
            Console.WriteLine("g = " + g);

            HashSet<BigInteger> set = new HashSet<BigInteger>();
            BigInteger X;

            for (int x = 1; x < N; x++)
            {
                X = BigInteger.Pow(g, x) % N;
                Console.WriteLine("X = " + X);
                if (!set.Contains(X)) {
                    set.Add(X);
                }
                else
                {
                    cycleNumber++;
                    goto AGAIN;
                }

            }

            Console.WriteLine("done");
            */


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

        static BigInteger SimpleNumber(BigInteger randnum)
        {
            while (!IsPrime(randnum))
            {
                randnum++;
            }
            return randnum;
        }

        public static bool IsPrime(BigInteger num)
        {
            bool CheckPrimeNumber = true;
            BigInteger factor = num / 2;
            BigInteger i = 0;
            for (i = 2; i <= factor; i++)
            {
                if ((num % i) == 0) CheckPrimeNumber = false;
            }
            if (num < 2)
                CheckPrimeNumber = false;
            return CheckPrimeNumber;
        }
    }
}
