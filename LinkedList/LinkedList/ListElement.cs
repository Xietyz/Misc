using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    public class ListElement
    {
        public ListElement next;
        public int pointer;
        public string value;
        public ListElement(string newVal)
        {
            value = newVal;
        }
        public ListElement(){}
    }
}
