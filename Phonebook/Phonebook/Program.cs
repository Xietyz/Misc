using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook
{
    class Program
    {
        const int StringSizeLimit = 4;
        static Dictionary<string, long> ContactDict;
        static Dictionary<string, Func<string, string>> FuncDict;
        static void Main(string[] args)
        {
            InitDicts();
            Console.WriteLine("--- PHONE BOOK ---");
            Console.WriteLine("COMMANDS: GET, STORE, DEL, UPDATE");
            string [] input = Console.ReadLine().Split(" ");
            string command = input[0];
            string value = input[1];
            Console.WriteLine(FuncDict["GET"]("123"));
        }
        public static void InitDicts()
        {
            ContactDict = new Dictionary<string, long>();
            ContactDict.Add("contact1", 123);
            FuncDict = new Dictionary<string, Func<string, string>>();

            FuncDict.Add("GET", GetName);
        }
        public static string GetName(string numberToCheck)
        {
            long number = long.Parse(numberToCheck);
            string name = "";
            try
            {
                name = ContactDict.First(x => x.Value == number).Key;
            }
            catch
            {
                Console.WriteLine("No contact with number " + number);
            }
            return name;
        }

        public static void StringSizeCheck(string stringToCheck)
        {
            if (stringToCheck.Length > StringSizeLimit)
            {
                throw new ArgumentException(stringToCheck + " is over character limit of " + StringSizeLimit);
            }
        }
    }
}
