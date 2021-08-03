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

            myList.push("4th el");
            myList.printAll();
            Console.WriteLine("3rd Element: " + myList.getEle(3).value);
            Console.WriteLine("DELETING SECOND ELEMENT: ");
            myList.deleteEle(0);
            myList.printAll();
            Console.WriteLine("ADDING NEW 5TH ELEMENT: ");
            myList.push("5th ele");
            myList.printAll();
            // write tests? 
        }
    }
}
