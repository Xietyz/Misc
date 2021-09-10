﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonebook
{
    public class FunctionDictionary
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
            if (StringSizeCheck(InputValue))
            {
                return "Name too large";
            };
            try
            {
                return ContactDict[InputValue].ToString();
            }
            catch
            {
                Console.WriteLine("No contact " + InputValue);
                return null;
            }
        }
        public string StoreContact()
        {
            if (NumberOnlyHasDigitsCheck(InputValue.Split(" ")[1]))
            {
                return "Number should only be digits";
            }
            long newNumber = long.Parse(InputValue.Split(" ")[1]);
            string newName = InputValue.Split(" ")[0];
            if (StringSizeCheck(newName)) 
            {
                return "Name too large";
            };
            if (NumberSizeCheck(newNumber))
            {
                return "Number too large";
            };


            ContactDict.Add(newName, newNumber);
            return "Stored " + newName + " - " + newNumber;
        }
        public string DeleteContact()
        {
            string nameToDelete = InputValue;
            ContactDict.TryGetValue(nameToDelete, out long numberToDelete);
            if (!ContactDict.ContainsKey(nameToDelete))
            {
                return "Does not exist";
            }
            ContactDict.Remove(nameToDelete);
            return "Deleted " + numberToDelete.ToString();
        }
        public string UpdateNumber()
        {
            if (NumberOnlyHasDigitsCheck(InputValue.Split(" ")[1]))
            {
                return "Number should only be digits";
            }
            long numberToUpdate = long.Parse(InputValue.Split(" ")[1]);
            string whoseNumberToUpdate = InputValue.Split(" ")[0];
            if (NumberSizeCheck(numberToUpdate))
            {
                return "Number too large";
            };
            if (!ContactDict.ContainsKey(whoseNumberToUpdate))
            {
                return "Does not exist";
            }
            ContactDict.TryGetValue(whoseNumberToUpdate, out long oldNumber);
            ContactDict[whoseNumberToUpdate] = numberToUpdate;
            return "UPDATED FROM " + oldNumber;
        }
        public bool StringSizeCheck(string stringToCheck)
        {
            if (stringToCheck.Length > StringSizeLimit)
            {
                return true;
                //throw new ArgumentException(stringToCheck + " is over character limit of " + StringSizeLimit);
            }
            return false;
        }
        public bool NumberSizeCheck(long numberToCheck)
        {
            if (numberToCheck.ToString().Length > 11)
            {
                return true;
                //throw new ArgumentException("Number too large");
            }
            return false;
        }
        public bool NumberOnlyHasDigitsCheck(string numberToCheck)
        {
            if (!numberToCheck.All(char.IsDigit))
            {
                return true;
            }
            return false;
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
            if (!Functions.ContainsKey(InputCommand))
            {
                return "Invalid input";
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