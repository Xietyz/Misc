using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var people = new List<Person>()
        {
            new Person("Bill", "Smith", 41),
            new Person("Sarah", "Jones", 22),
            new Person("Stacy","Baker", 21),
            new Person("Vivianne","Dexter", 19 ),
            new Person("Bob","Smith", 49 ),
            new Person("Brett","Baker", 51 ),
            new Person("Mark","Parker", 19),
            new Person("Alice","Thompson", 18),
            new Person("Evelyn","Thompson", 58 ),
            new Person("Mort","Martin", 58),
            new Person("Eugene","deLauter", 84 ),
            new Person("Gail","Dawson", 19 ),
        };

        PrintPeopleByFirstNameOrder(people);
        PrintLastNameStartsWithD(people);
        PrintSurnameMatch(people);
        ListToDictionary(people);
        PrintFirstPersonOver40Descending(people);
        PrintFamilies(people);
        PrintMultiplesOf4And6();
        PrintMoneySum();
    }

    private static void PrintMoneySum()
    {
        // 7. How much money have we made?
        List<double> purchases = new List<double>()
            {
                2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
            };
        Console.WriteLine("We have made: " + purchases.Sum());
    }

    private static void PrintMultiplesOf4And6()
    {
        //6. Write a linq statement that finds which of the following numbers are multiples of 4 or 6
        List<int> mixedNumbers = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };
        var multiples = mixedNumbers.Where(x => x % 4 == 0 || x % 6 == 0);
        foreach (var number in multiples)
        {
            Console.WriteLine(number);
        }
        Console.WriteLine();
    }

    private static void PrintFamilies(List<Person> people)
    {
        //5. write a linq statement that finds all the people who are part of a family. (aka there is at least one other person with the same surname.
        var families = people.GroupBy(x => x.LastName).Where(x => x.Count() > 1);
        foreach (var family in families)
        {
            foreach (var person in family)
            {
                Console.WriteLine(person.ToString());
            }
        }
        Console.WriteLine();
    }

    private static void PrintFirstPersonOver40Descending(List<Person> people)
    {
        // 4. Write linq statement for first Person Older Than 40 In Descending Alphabetical Order By First Name
        Person personOver40 = people.Where(x => x.Age > 40).OrderByDescending(x => x.FirstName).First();
        Console.WriteLine(personOver40.ToString());
        Console.WriteLine();
    }

    private static void ListToDictionary(List<Person> people)
    {
        //3. write linq to convert the list of people to a dictionary keyed by first name
        var peopleDictionary = people.ToDictionary(x => x.FirstName);
        peopleDictionary.TryGetValue("Mort", out Person dictPerson);
        Console.WriteLine(dictPerson.ToString());
    }

    private static void PrintSurnameMatch(List<Person> people)
    {
        //2. write linq statement for all the people who are have the surname Thompson and Baker. Write all the first names to the console
        var peopleList2 = people.Where(x => x.LastName == "Thompson" || x.LastName == "Baker");
        foreach (Person person in peopleList2)
        {
            Console.WriteLine(person.ToString());
        }
        Console.WriteLine();
    }

    private static void PrintLastNameStartsWithD(List<Person> people)
    {
        //1. write linq statement for the people with last name that starts with the letter D
        var peopleList1 = people.Where(x => x.LastName.Substring(0, 1) == "D");
        foreach (Person person in peopleList1)
        {
            Console.WriteLine(person.ToString());
        }
        Console.WriteLine();
    }

    private static void PrintPeopleByFirstNameOrder(List<Person> people)
    {
        //1. write linq display every name ordered alphabetically
        var peopleList = people.OrderBy(x => x.FirstName);
        foreach (Person person in peopleList)
        {
            Console.WriteLine(person.ToString());
        }
        Console.WriteLine();
    }

    public class Person
    {
        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}