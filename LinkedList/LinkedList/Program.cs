using System;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            //LList<string> myList = new LList<string>();

            //myList.Add("1st");
            //myList.Add("2nd");
            //myList.Add("3rd");
            //myList.Add("4th");
            //myList.PrintAll();
            //Console.WriteLine();

            //myList.Add(0, "NEW 1");
            //myList.PrintAll();
            //Console.WriteLine();

            //myList.Add("4th", "NEW 2");
            //myList.PrintAll();
            //Console.WriteLine();

            //myList.Delete(4);
            //myList.PrintAll();
            //Console.WriteLine();

            //myList.Delete("3rd");
            //myList.PrintAll();
            //Console.WriteLine();

            //myList.Replace("NEW 1", "REPLACED");
            //myList.PrintAll();
            //Console.WriteLine();

            //Console.WriteLine(myList.Get(0).value);

            LList<string> bigramList = new LList<string>();
            bigramList.Add("Sean Sean");
            bigramList.Add("Sean Sean");
            bigramList.Add("Broseph");
            bigramList.PrintBigrams();
        }
    }
}
