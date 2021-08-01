using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    class ListElement
    {
        public ListElement next;
        public ListElement prev;
        public int pointer;
        public string value;
        public ListElement(ListElement newPrev, string newVal, int newPointer)
        {
            prev = newPrev;
            value = newVal;
            pointer = newPointer;
        }
        public ListElement(string newVal, int newPointer)
        {
            value = newVal;
            pointer = newPointer;
        }
        public ListElement(){}
    }
}
