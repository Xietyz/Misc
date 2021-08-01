using System;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            LList myList = new LList(3);
            myList.push("First el");
            myList.push("Second El");
            myList.push("Third el");

            myList.printAll();
        }
    }
}
