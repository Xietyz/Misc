using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook
{
    class Contact
    {
        public Contact() {}
        public Contact(string newName, long newNumber)
        {
            Name = newName;
            PhoneNumber = newNumber;
        }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
    }
}
