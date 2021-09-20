using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Phonebook
{
    public class FunctionDictionary
    {
        const int StringSizeLimit = 4;
        public Dictionary<string, Func<string>> Functions;
        public Dictionary<int, long> ContactDict;
        const string ContactFileDir = "/Contacts.txt";
        string InputCommand;
        string InputValue;
        public FunctionDictionary()
        {
            InitDicts();
            ReadContactFileToDictionary();
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
        public string GetNumber()
        {
            try
            {
                return ContactDict[GetStableHashCode(InputValue)].ToString();
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
            if (NumberSizeCheck(newNumber))
            {
                return "Number too large";
            };
            ContactDict.Add(GetStableHashCode(newName), newNumber);

            SaveContactsToFile(ContactDict);
            return "Stored " + newName + " - " + newNumber;
        }
        public string DeleteContact()
        {
            string nameToDelete = InputValue;
            ContactDict.TryGetValue(GetStableHashCode(nameToDelete), out long numberToDelete);
            if (!ContactDict.ContainsKey(GetStableHashCode(nameToDelete)))
            {
                return "Does not exist";
            }
            ContactDict.Remove(GetStableHashCode(nameToDelete));

            SaveContactsToFile(ContactDict);
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
            if (!ContactDict.ContainsKey(GetStableHashCode(whoseNumberToUpdate)))
            {
                return "Does not exist";
            }
            ContactDict.TryGetValue(GetStableHashCode(whoseNumberToUpdate), out long oldNumber);
            ContactDict[GetStableHashCode(whoseNumberToUpdate)] = numberToUpdate;

            SaveContactsToFile(ContactDict);
            return "UPDATED FROM " + oldNumber;
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
        public void SaveContactsToFile(Dictionary<int, long> contactDict)
        {
            foreach (var contact in contactDict)
            {
                using (StreamWriter file = new StreamWriter(ContactFileDir))
                {
                    file.WriteLine(contact.Key + "," + contact.Value);
                }
            }
        }
        public void ReadContactFileToDictionary()
        {
            string[] allData;
            if (System.IO.File.Exists(ContactFileDir))
            {
                allData = System.IO.File.ReadAllLines(ContactFileDir);
                foreach (var contact in allData)
                {
                    string[] line = contact.Split(",");
                    ContactDict.Add(int.Parse(line[0]), long.Parse(line[1]));
                }
            }
        }
        public int GetStableHashCode(string str)
        {
            {
                int hash1 = 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length && str[i] != '\0'; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1 || str[i + 1] == '\0')
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }
    }
}
