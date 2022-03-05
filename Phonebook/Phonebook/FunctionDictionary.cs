using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Phonebook
{
    public class FunctionDictionary
    {
        public Dictionary<string, Func<string>> Functions;
        public Dictionary<int, long> ContactDict;
        string InputCommand;
        string InputValue;
        PhonebookService Service;
        PhonebookFileReader FileService;

        public FunctionDictionary(PhonebookFileReader reader)
        {
            FileService = reader;
            Service = new PhonebookService(FileService);
            InitDicts();
        }
        public void InitDicts()
        {
            ContactDict = new Dictionary<int, long>();
            Functions = new Dictionary<string, Func<string>>();

            Functions.Add("GET", GetNumber);
            Functions.Add("STORE", StoreContact);
            Functions.Add("DELETE", DeleteContact);
            Functions.Add("UPDATE", UpdateNumber);
        }

        private string UpdateNumber()
        {
            return Service.UpdateNumber(ContactDict, InputValue);
        }

        private string DeleteContact()
        {
            try
            {
                return Service.DeleteContact(ContactDict, InputValue);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete contact " + InputValue + ", ERROR:", ex);
            }
        }

        private string StoreContact()
        {
            try
            {
                return Service.StoreContact(ContactDict, InputValue);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create contact " + InputValue + ", ERROR:", ex);
            }
        }

        public string GetNumber()
        {
            try
            {
                return Service.GetNumber(ContactDict, InputValue);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get contact " + InputValue + ", ERROR:", ex);
            }
        }
        public string Execute(string input)
        {
            string[] inputArray = new string[] { };
            try
            {
                inputArray = input.Split(" ");
                InputCommand = inputArray[0].ToUpper();
            }
            catch (Exception ex)
            {
                throw new Exception("Bad command", ex);
            } 
            
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

    }
}
