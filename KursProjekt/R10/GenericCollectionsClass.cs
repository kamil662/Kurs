using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Generyczne klasy:
 * - List<T> - sekwencyja lista elementów z dynamiczną zmianą rozmiarów;
 * - Queue<T> - Generyczna implementacja listy FIFO;
 * - Stack<T> - Generyczna implementacja listy LIFO;
 * - SortedSet - Kolekcja posortowanych obiektów bez duplikatów;
 * - Dictionary<TKey,TValue> - Generyczna kolekcja kluczy i wartości;
 * - LinkedList<T> - Lista podwójnie wiązana;
 * - SortedDictionary<TKey,TValue> - Generyczny posortowany zbiór par klucz/wartość;
 * 
 * 
 * */

namespace KursProjekt.R10
{
    class GenericCollectionsClass
    {
        public void WorkMethod()
        {
            Console.WriteLine("***** Fun with Generic Collections *****\n");
            // UseGenericList();
            // UseGenericQueue();
            // UseGenericStack();
            UseSortedSet();
            Console.ReadLine();
        }

        #region Use List<T>

        private static void UseGenericList()
        {
            // Make a List of Person objects, filled with 
            // collection / object init syntax.
            List<Person> people = new List<Person>()
            {
                new Person {FirstName= "Homer", LastName="Simpson", Age=47},
                new Person {FirstName= "Marge", LastName="Simpson", Age=45},
                new Person {FirstName= "Lisa", LastName="Simpson", Age=9},
                new Person {FirstName= "Bart", LastName="Simpson", Age=8}
            };

            // Liczba elementów na liście
            Console.WriteLine("Items in list: {0}", people.Count);

            // Enumerate over list.
            foreach (Person p in people)
                Console.WriteLine(p);

            // Insert a new person.
            Console.WriteLine("\n->Inserting new person.");
            Person newPerson = new Person { FirstName = "Maggie", LastName = "Simpson", Age = 2 };
            people.Insert(2, newPerson);
            Console.WriteLine("Items in list: {0}", people.Count);

            // Copy data into a new array.
            Person[] arrayOfPeople = people.ToArray();
            for (int i = 0; i < arrayOfPeople.Length; i++)
            {
                Console.WriteLine("First Names: {0}",
                  arrayOfPeople[i].FirstName);
            }

            people.Sort(new SortPeopleByAge());
            int indexNewPerson = people.IndexOf(newPerson);
            int ilosc = people.Count;

        }
        #endregion

        #region Use Queue<T>
        static void GetCoffee(Person p)
        {
            Console.WriteLine("{0} got coffee!", p.FirstName);
        }

        static void UseGenericQueue()
        {
            // Make a Q with three people.
            Queue<Person> peopleQ = new Queue<Person>();
            peopleQ.Enqueue(new Person { FirstName = "Homer", LastName = "Simpson", Age = 47 });
            peopleQ.Enqueue(new Person { FirstName = "Marge", LastName = "Simpson", Age = 45 });
            peopleQ.Enqueue(new Person { FirstName = "Lisa", LastName = "Simpson", Age = 9 });

            // Peek() - zwraca pierwszą osobę na kolejce, nie usuwając jej
            Console.WriteLine("{0} is first in line!", peopleQ.Peek().FirstName);

            // Usuwanie za pomocą pomocniczej funkcji, 
            // która obsługuje wyjątek w przypadku próby usunięcia elementu z pustej kolejki
            GetCoffee(peopleQ.Dequeue());
            GetCoffee(peopleQ.Dequeue());
            GetCoffee(peopleQ.Dequeue());

            try
            {
                GetCoffee(peopleQ.Dequeue());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Error! {0}", e.Message);
            }
        }
        #endregion

        #region Use Stack<T>
        static void UseGenericStack()
        {
            Stack<Person> stackOfPeople = new Stack<Person>();
            stackOfPeople.Push(new Person { FirstName = "Homer", LastName = "Simpson", Age = 47 });
            stackOfPeople.Push(new Person { FirstName = "Marge", LastName = "Simpson", Age = 45 });
            stackOfPeople.Push(new Person { FirstName = "Lisa", LastName = "Simpson", Age = 9 });

            // Now look at the top item, pop it, and look again.
            Console.WriteLine("First person is: {0}", stackOfPeople.Peek());
            Console.WriteLine("Popped off {0}", stackOfPeople.Pop());

            Console.WriteLine("\nFirst person is: {0}", stackOfPeople.Peek());
            Console.WriteLine("Popped off {0}", stackOfPeople.Pop());

            Console.WriteLine("\nFirst person item is: {0}", stackOfPeople.Peek());
            Console.WriteLine("Popped off {0}", stackOfPeople.Pop());

            try
            {
                Console.WriteLine("\nnFirst person is: {0}", stackOfPeople.Peek());
                Console.WriteLine("Popped off {0}", stackOfPeople.Pop());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("\nError! {0}", ex.Message);
            }
        }

        #endregion

        #region Use SortedSet<T>

        // Klasa generyczna SortedSet<T> zapewnia automatyczne sortowanie obiektów wstawianych do kolekcji
        // należy jedynie do konstruktora przesłać obiekt implementujący interfejs IComparer<T>
        // który definiuje porównywanie obiektów.
        private static void UseSortedSet()
        {
            // Make some people with different ages.
            SortedSet<Person> setOfPeople = new SortedSet<Person>(new SortPeopleByAge())
            {
                new Person {FirstName= "Homer", LastName="Simpson", Age=47},
                new Person {FirstName= "Marge", LastName="Simpson", Age=45},
                new Person {FirstName= "Lisa", LastName="Simpson", Age=9},
                new Person {FirstName= "Bart", LastName="Simpson", Age=8}
            };

            // Osoby od razu są posortowane wg wieku
            foreach (Person p in setOfPeople)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine();

            // Dodanie osób w róznym wieku
            setOfPeople.Add(new Person { FirstName = "Saku", LastName = "Jones", Age = 1 });
            setOfPeople.Add(new Person { FirstName = "Mikko", LastName = "Jones", Age = 32 });

            foreach (Person p in setOfPeople)
            {
                Console.WriteLine(p);
            }
        }

        #endregion

    }


    #region Person class for testing
    public class Person
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person() { }
        public Person(string firstName, string lastName, int age)
        {
            Age = age;
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} {1}, Age: {2}",
              FirstName, LastName, Age);
        }
    }
    #endregion

    #region SortPeopleByAge class
    class SortPeopleByAge : IComparer<Person>
    {
        public int Compare(Person firstPerson, Person secondPerson)
        {
            if (firstPerson.Age > secondPerson.Age)
                return 1;
            if (firstPerson.Age < secondPerson.Age)
                return -1;
            else
                return 0;
        }
    }
    #endregion

}