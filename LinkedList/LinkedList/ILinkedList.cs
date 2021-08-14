using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    public interface ILinkedList<T>
    {
        /// <summary>
        /// add at position
        /// </summary>
        void Add(int postion, T data);
        /// <summary>
        /// add after data
        /// </summary>
        void Add(T dataToAddAfter, T nextData);
        /// <summary>
        /// add at start
        /// </summary>
        void Add(T data);


        void Delete(int postion);
        void Delete(T data);

        ListElement<T> Get(int position);

        void Replace(T oldData, T newData);
    }
}
