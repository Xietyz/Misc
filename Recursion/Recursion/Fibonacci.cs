using System;
using System.Collections.Generic;
using System.Text;

namespace Recursion
{
    class Fibonacci
    {
        int _limit;
        int _count = 0;
        public Fibonacci(int limit)
        {
            _limit = limit;
        }
        public int FibonacciRecursion(int value1, int value2)
        {
            int sum = value1 + value2;
            Console.WriteLine("Step " + _count + ": " + sum);
            _count++;
            if (_count == _limit)
            {
                return sum;
            }
            return FibonacciRecursion(value2, sum);
        }
    }
}
