using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook
{
    class Program
    {
        static void Main(string[] args)
        {
            FunctionDictionary functionDict = new FunctionDictionary(new PhonebookFileReader());
            Console.WriteLine("--- PHONE BOOK ---");
            Console.WriteLine("COMMANDS: GET, STORE, DELETE, UPDATE");
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
