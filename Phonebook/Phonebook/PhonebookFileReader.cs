using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Phonebook
{
    public class PhonebookFileReader
    {
        const string ContactFileDir = "/Contacts.txt";
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
        public void ReadContactFileToDictionary(Dictionary<int, long> contactDict)
        {
            string[] allData;
            if (System.IO.File.Exists(ContactFileDir))
            {
                allData = System.IO.File.ReadAllLines(ContactFileDir);
                foreach (var contact in allData)
                {
                    string[] line = contact.Split(",");
                    contactDict.Add(int.Parse(line[0]), long.Parse(line[1]));
                }
            }
        }
    }
}
