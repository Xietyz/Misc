using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    class LList : ILinkedList
    {
        ListElement _firstElement;
        public void Add(int position, string data)
        {
            // at position
            ListElement newElement = new ListElement(data);
            ListElement currentElement = _firstElement;
            if (position == 0)
            {
                newElement.next = currentElement;
                _firstElement = newElement;
            }
            else
            {
                for (int x = 1; x < position; x++)
                {
                    if (currentElement.next == null)
                    {
                        throw new Exception("Out of bounds");
                    }
                    currentElement = currentElement.next;
                }
                newElement.next = currentElement.next;
                currentElement.next = newElement;
            }
        }
        public void Add(string dataToAddAfter, string nextData)
        {
            // add after found data
            ListElement currentElement = _firstElement;
            while (currentElement.value != dataToAddAfter)
            {
                if (currentElement.next == null)
                {
                    throw new Exception("Out of bounds");
                }
                currentElement = currentElement.next;
            }
            ListElement newElement = new ListElement(nextData);
            newElement.next = currentElement.next;
            currentElement.next = newElement;
        }
        public void Add(string data)
        {
            // add at start
            if (_firstElement == null)
            {
                _firstElement = new ListElement(data);
            }
            else
            {
                ListElement newElement = new ListElement(data);
                newElement.next = _firstElement;
                _firstElement = newElement;
            }
        }
        public void Delete(int position)
        {
            ListElement elementToDelete = _firstElement;
            ListElement previousElement = _firstElement;
            for (int x = 0; x < position; x++)
            {
                previousElement = elementToDelete;
                elementToDelete = elementToDelete.next;
            }
            if (elementToDelete == _firstElement)
            {
                _firstElement = _firstElement.next;
            }
            else
            {
                previousElement.next = elementToDelete.next;
            }
        }
        public void Delete(string data)
        {
            ListElement elementToDelete = _firstElement;
            ListElement previousElement = _firstElement;
            while (elementToDelete.value != data)
            {
                previousElement = elementToDelete;
                elementToDelete = elementToDelete.next;
            }
            if (elementToDelete.value.Equals(data))
            {
                if (elementToDelete == _firstElement)
                {
                    _firstElement = _firstElement.next;
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
            ListElement elementToReplace = _firstElement;
            ListElement previousElement = null;
            if (_firstElement.value == oldData)
            {
                newElement.next = _firstElement.next;
                _firstElement = newElement;
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
            ListElement returnElement = _firstElement;
            for (int x = 0; x < position; x++)
            {
                returnElement = returnElement.next;
            }
            return returnElement;
        }
        public void PrintAll()
        {
            ListElement currentEl = _firstElement;
            while (currentEl != null)
            {
                Console.WriteLine(currentEl.value);
                currentEl = currentEl.next;
            }
        }
    }
}
