using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    public interface ILinkedList
    {
        /// <summary>
        /// add at position
        /// </summary>
        void Add(int postion, string data);
        /// <summary>
        /// add after data
        /// </summary>
        void Add(string dataToAddAfter, string nextData);
        /// <summary>
        /// add at start
        /// </summary>
        void Add(string data);


        void Delete(int postion);
        void Delete(string data);

        ListElement Get(int position);

        void Replace(string oldData, string newData);
    }
}
