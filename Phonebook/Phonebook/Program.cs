using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook
{
    class Program
    {
        const int StringSizeLimit = 4;
        static void Main(string[] args)
        {
            FunctionDictionary functionDict = new FunctionDictionary();
            Console.WriteLine("--- PHONE BOOK ---");
            Console.WriteLine("COMMANDS: GET, STORE, DEL, UPDATE, EXIT");
            Console.WriteLine();
            string input = "";
            while (!input.Equals("EXIT"))
            {
                input = Console.ReadLine();
                Console.WriteLine(functionDict.Execute(input));
                Console.WriteLine("------------");
            }
            
        }
    }
}
