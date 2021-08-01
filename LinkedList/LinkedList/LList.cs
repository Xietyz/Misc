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
            if(maxPointer > values.Length)
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
                newEl = new ListElement(values[maxPointer - 1], val, maxPointer);
                newEl.prev.next = newEl;
            }
            values[maxPointer] = newEl;
            maxPointer++;
        }
        // get
        public ListElement getEle(int desiredElePos)
        {
            ListElement returnEl = new ListElement();
            //for (int x = 0; x < values.Length; x++)
            //{
            //    if (values[x].pointer == desiredElePos)
            //    {
            //        return values[x];
            //    }
            //}
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
            for (int x = 0; x < original.Length; x++)
            {
                tempValues[x] = original[x];
            };
            return tempValues;
        }
    }
}
