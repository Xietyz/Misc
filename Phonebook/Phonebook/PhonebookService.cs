using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonebook
{
    public class PhonebookService
    {
        PhonebookFileReader FileReader;
        public PhonebookService(PhonebookFileReader fileReader)
        {
            FileReader = fileReader;
        }
        public string GetNumber(Dictionary<int, long> contactDict, string inputValue)
        {
            try
            {
                return contactDict[GetStableHashCode(inputValue)].ToString();
            }
            catch
            {
                return null;
            }
        }
        public string UpdateNumber(Dictionary<int, long> contactDict, string inputValue)
        {
            if (NumberOnlyHasDigitsCheck(inputValue.Split(" ")[1]))
            {
                return "Number should only be digits";
            }
            long numberToUpdate = long.Parse(inputValue.Split(" ")[1]);
            string whoseNumberToUpdate = inputValue.Split(" ")[0];
            if (NumberSizeCheck(numberToUpdate))
            {
                return "Number too large";
            };
            if (!contactDict.ContainsKey(GetStableHashCode(whoseNumberToUpdate)))
            {
                return "Does not exist";
            }
            contactDict.TryGetValue(GetStableHashCode(whoseNumberToUpdate), out long oldNumber);
            contactDict[GetStableHashCode(whoseNumberToUpdate)] = numberToUpdate;

            FileReader.SaveContactsToFile(contactDict);
            return "UPDATED FROM " + oldNumber;
        }
        public string DeleteContact(Dictionary<int, long> contactDict, string inputValue)
        {
            string nameToDelete = inputValue;
            contactDict.TryGetValue(GetStableHashCode(nameToDelete), out long numberToDelete);
            if (!contactDict.ContainsKey(GetStableHashCode(nameToDelete)))
            {
                return "Does not exist";
            }
            contactDict.Remove(GetStableHashCode(nameToDelete));

            FileReader.SaveContactsToFile(contactDict);
            return "Deleted " + numberToDelete.ToString();
        }
        public string StoreContact(Dictionary<int, long> contactDict, string inputValue)
        {
            if (NumberOnlyHasDigitsCheck(inputValue.Split(" ")[1]))
            {
                return "Number should only be digits";
            }
            long newNumber = long.Parse(inputValue.Split(" ")[1]);
            string newName = inputValue.Split(" ")[0];
            if (NumberSizeCheck(newNumber))
            {
                return "Number too large";
            };
            contactDict.Add(GetStableHashCode(newName), newNumber);

            FileReader.SaveContactsToFile(contactDict);
            return "Stored " + newName + " - " + newNumber;
        }
        public bool NumberSizeCheck(long numberToCheck)
        {
            if (numberToCheck.ToString().Length > 11)
            {
                return true;
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
