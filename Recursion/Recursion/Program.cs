using System;

namespace Recursion
{
    class Program
    {
        static void Main(string[] args)
        {
            int steps = 10;
            int result = new Fibonacci(steps).FibonacciRecursion(0, 1);
            Console.WriteLine(result);
        }
    }
}
