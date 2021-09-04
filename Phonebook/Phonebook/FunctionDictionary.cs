using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonebook
{
    class FunctionDictionary
    {
        const int StringSizeLimit = 4;
        public Dictionary<string, Func<string>> Functions;
        public Dictionary<string, long> ContactDict;
        string InputCommand;
        string InputValue;
        public FunctionDictionary()
        {
            InitDicts();
        }
        public void InitDicts()
        {
            ContactDict = new Dictionary<string, long>();
            Functions = new Dictionary<string, Func<string>>();

            ContactDict.Add("con1", 123);
            Functions.Add("GET", GetNumber);
            Functions.Add("STORE", StoreContact);
            Functions.Add("DELETE", DeleteContact);
            Functions.Add("UPDATE", UpdateNumber);
        }
        public string GetNumber()
        {
            StringSizeCheck(InputValue);
            long numberToReturn = 0;
            try
            {
                ContactDict.TryGetValue(InputValue, out numberToReturn);
            }
            catch
            {
                Console.WriteLine("No contact " + InputValue);
            }
            return numberToReturn.ToString();
        }
        public string StoreContact()
        {
            string newName = InputValue.Split(" ")[0]; ;
            long newNumber = long.Parse(InputValue.Split(" ")[1]);
            StringSizeCheck(newName);
            NumberSizeCheck(newNumber);

            ContactDict.Add(newName, newNumber);
            return "Stored " + newName + " - " + newNumber;
        }
        public string DeleteContact()
        {
            string nameToDelete = InputValue;
            ContactDict.TryGetValue(nameToDelete, out long numberToDelete);
            ContactDict.Remove(nameToDelete);
            return "DELETED " +numberToDelete.ToString();
        }
        public string UpdateNumber()
        {
            string whoseNumberToUpdate = InputValue.Split(" ")[0];
            long numberToUpdate = long.Parse(InputValue.Split(" ")[1]);
            NumberSizeCheck(numberToUpdate);
            ContactDict.TryGetValue(whoseNumberToUpdate, out long oldNumber);
            ContactDict[whoseNumberToUpdate] = numberToUpdate;
            return "UPDATED FROM " + oldNumber;
        }
        public void StringSizeCheck(string stringToCheck)
        {
            if (stringToCheck.Length > StringSizeLimit)
            {
                throw new ArgumentException(stringToCheck + " is over character limit of " + StringSizeLimit);
            }
        }
        public void NumberSizeCheck(long numberToCheck)
        {
            if (numberToCheck.ToString().Length <= 11)
            {
                throw new ArgumentException("Number too large");
            }
        }
        public string Execute(string input)
        {
            string[] inputArray = input.Split(" ");
            InputCommand = inputArray[0].ToUpper();
            if (input.Split(" ").Count() > 2)
            {
                InputValue = inputArray[1] + " " + inputArray[2];
                Console.WriteLine(InputValue);
            } 
            else if (input.Split(" ").Count() == 2)
            {
                InputValue = input.Split(" ")[1];
            }
            else
            {
                return "EXITING";
            }
            
            return Functions[InputCommand]();
        }
        //public string CommandFromInput(string input)
        //{
        //    return input.Split(" ")[0];
        //}
        //public string ValueFromInput(string input)
        //{
        //    return input.Split(" ")[1];
        //}
    }
}
