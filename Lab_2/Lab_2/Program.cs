﻿using System;

namespace Lab_2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            long a;
            long b;
            long g;
            long p;
            long A;
            long B;
            long K1;
            long K2;

            Console.WriteLine("Введите число Алисы: ");
            g = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("Введите число Боба: ");
            p = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("Введите секретное число Алисы: ");
            a = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("Введите секретное число Боба: ");
            b = Convert.ToInt64(Console.ReadLine());

            A = g ^ a % p;
            B = g ^ b % p;

            K1 = B ^ a % p;
            K2 = A ^ b % p;

            Console.WriteLine("a: " + a);
            Console.WriteLine("b: " + b);
            Console.WriteLine("g: " + g);
            Console.WriteLine("p: " + p);
            Console.WriteLine("A: " + A);
            Console.WriteLine("B: " + B);
            Console.WriteLine("K1: " + K1);
            Console.WriteLine("K1: " + K1);
        }
    }
}
