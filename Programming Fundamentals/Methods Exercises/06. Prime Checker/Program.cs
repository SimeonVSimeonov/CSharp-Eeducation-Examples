﻿using System;

namespace _06._Prime_Checker
{
    class Program
    {
        static void Main(string[] args)
        {
            long num = long.Parse(Console.ReadLine());
            Console.WriteLine(IsPrime(num));
        }

        private static bool IsPrime(long num)
        {
            int sqrtNum = (int)Math.Sqrt(num);
            if (num <= 1)
            {
                return false;
            }
            else if (num > 2)
            {
                for (int i = 2; i <= sqrtNum; i++)
                {
                    if (num % i == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
