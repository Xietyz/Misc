using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    class LList
    {
        public int maxPointer = 0;
        ListElement[] values;
        ListElement firstEl;
        ListElement lastEl;
        public LList(int listSize)
        {
            values = new ListElement[listSize];
        }
        public void push(string val)
        {
            if (maxPointer > values.Length - 1)
            {
                values = copy(values.Length + 1, values);
            };
            ListElement newEl;
            if (maxPointer == 0)
            {
                newEl = new ListElement(val, maxPointer);
                firstEl = newEl;
            } else
            {
                ListElement TESTPREV = values[maxPointer - 1];
                newEl = new ListElement(values[maxPointer - 1], val, maxPointer);
                TESTPREV.next = newEl;
            }
            values[maxPointer] = newEl;
            maxPointer++;
        }
        public void deleteEle(int elePos)
        {
            ListElement eleToDelete = firstEl;
            for (int x = 0; x < elePos; x++)
            {
                eleToDelete = eleToDelete.next;
            }
            if (eleToDelete == firstEl)
            {
                firstEl = firstEl.next;
            } 
            else
            {
                ListElement prevEle = eleToDelete.prev;
                ListElement nextEle = eleToDelete.next;
                prevEle.next = nextEle;
                nextEle.prev = prevEle;
            }
            maxPointer--;
            values = copy(values.Length - 1, values);
        }
        public ListElement getEle(int elePos)
        {
            ListElement returnEl = firstEl;
            for (int x = 0; x < elePos - 1; x++)
            {
                returnEl = returnEl.next;
            }
            return returnEl;
        }
        public void printAll()
        {
            ListElement currentEl = firstEl;
            while(currentEl != null)
            {
                Console.WriteLine(currentEl.value);
                currentEl = currentEl.next;
            }
        }
        public ListElement[] copy(int newSize, ListElement[] original)
        {
            ListElement[] tempValues = new ListElement[newSize];
            ListElement newElement = firstEl;
            for (int x = 0; x < original.Length; x++)
            {
                if (newElement != null)
                {
                    tempValues[x] = newElement;
                    newElement = newElement.next;
                }
            }
            return tempValues;
        }
    }
}
