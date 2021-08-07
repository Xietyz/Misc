using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    class LList : ILinkedList
    {
        ListElement firstElement;
        public void Add(int position, string data)
        {
            // at position
            ListElement newElement = new ListElement(data);
            ListElement currentElement = firstElement;
            if (position == 0)
            {
                newElement.next = currentElement;
                firstElement = newElement;
            }
            else
            {
                for (int x = 1; x < position; x++)
                {
                    currentElement = currentElement.next;
                }
                newElement.next = currentElement.next;
                currentElement.next = newElement;
            }
        }
        public void Add(string dataToAddAfter, string nextData)
        {
            // add after found data
            ListElement currentElement = firstElement;
            while (currentElement.value != dataToAddAfter)
            {
                currentElement = currentElement.next;
            }
            ListElement newElement = new ListElement(nextData);
            newElement.next = currentElement.next;
            currentElement.next = newElement;
        }
        public void Add(string data)
        {
            // add at start
            if (firstElement == null)
            {
                firstElement = new ListElement(data);
            }
            else
            {
                ListElement newElement = new ListElement(data);
                newElement.next = firstElement;
                firstElement = newElement;
            }
        }
        public void Delete(int position)
        {
            ListElement elementToDelete = firstElement;
            ListElement previousElement = firstElement;
            for (int x = 0; x < position; x++)
            {
                previousElement = elementToDelete;
                elementToDelete = elementToDelete.next;
            }
            if (elementToDelete == firstElement)
            {
                firstElement = firstElement.next;
            }
            else
            {
                previousElement.next = elementToDelete.next;
            }
        }
        public void Delete(string data)
        {
            ListElement elementToDelete = firstElement;
            ListElement previousElement = firstElement;
            while (elementToDelete.value != data)
            {
                previousElement = elementToDelete;
                elementToDelete = elementToDelete.next;
            }
            if (elementToDelete.value.Equals(data))
            {
                if (elementToDelete == firstElement)
                {
                    firstElement = firstElement.next;
                }
                else
                {
                    previousElement.next = elementToDelete.next;
                }
            }
        }
        public void Replace(string oldData, string newData)
        {
            ListElement newElement = new ListElement(newData);
            ListElement elementToReplace = firstElement;
            ListElement previousElement = null;
            if (firstElement.value == oldData)
            {
                newElement.next = firstElement.next;
                firstElement = newElement;
            }
            else
            {
                while (elementToReplace.value != oldData)
                {
                    previousElement = elementToReplace;
                    elementToReplace = elementToReplace.next;
                }

                if (previousElement != null)
                {
                    previousElement.next = newElement;
                }
                newElement.next = elementToReplace.next;
                elementToReplace.next = newElement;
            }
        }
        public ListElement Get(int position)
        {
            ListElement returnElement = firstElement;
            for (int x = 0; x < position; x++)
            {
                returnElement = returnElement.next;
            }
            return returnElement;
        }
        public void PrintAll()
        {
            ListElement currentEl = firstElement;
            while (currentEl != null)
            {
                Console.WriteLine(currentEl.value);
                currentEl = currentEl.next;
            }
        }
    }
}
