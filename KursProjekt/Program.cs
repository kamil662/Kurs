using KursProjekt.R9;
using KursProjekt.R9.InterfejsWbudawanyNET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjekt 
{
    class Program
    {
        
        private static void IEnumerableT_ExampleWithYield()
        {
            MyList<string> myListOfStrings = new MyList<string>();

            myListOfStrings.Add("one");
            myListOfStrings.Add("two");
            myListOfStrings.Add("three");
            myListOfStrings.Add("four");

            foreach (string s in myListOfStrings)
            {
                Console.WriteLine(s);
            }
        }
        /*static void IComparableExamples()
        {
            Console.WriteLine("***** Fun with Object Sorting *****\n");
            // Make an array of Car types.
            Car[] myAutos = new Car[5];
            myAutos[0] = new Car("Rusty", 80, 1);
            myAutos[1] = new Car("Mary", 40, 234);
            myAutos[2] = new Car("Viper", 40, 34);
            myAutos[3] = new Car("Mel", 40, 4);
            myAutos[4] = new Car("Chucky", 40, 5);

            // Display current array.
            Console.WriteLine("Here is the unordered set of cars:");
            foreach (Car c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.PetName);
            Console.WriteLine();

            // Now, sort them using IComparable!
            Array.Sort(myAutos);

            // Display sorted array.
            Console.WriteLine("Here is the ordered set of cars:");
            foreach (Car c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.PetName);
            Console.WriteLine();

            // Now sort by CurrSpeed.
            Array.Sort(myAutos, Car.SortByCurrentSpeed);

            // Dump sorted array.
            Console.WriteLine("Ordering by pet name:");
            foreach (Car c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.PetName);

            Console.ReadLine();
        }*/

        static void IComparableT_Examples()
        {
            List<Cuboid> TabCube = new List<Cuboid>();
            int tmp = 0;
            Random rnd = new Random();
            for (int i = 0; i < 7; i++)
            {
                tmp = rnd.Next(0,20);
                TabCube.Add(new Cuboid(tmp, i + tmp, i * tmp));
            }
            Console.WriteLine("Before sorting:" + Environment.NewLine);

            foreach (Cuboid item in TabCube)
            {
                Console.WriteLine( item.ToString());
            }
            //TabCube.Sort
            comparerLength CubeComparerLenth = new comparerLength();
            TabCube.Sort(CubeComparerLenth);

            Console.WriteLine(Environment.NewLine+"After sorting:" + Environment.NewLine);
            foreach (Cuboid item in TabCube)
            {
                Console.WriteLine(item.ToString());
            }
        }

        static void staticIEnumerableT_Example()
        {
            var tmp = 1.To(20);
            foreach (var item in tmp)
            {
                Console.WriteLine(item);
            }
        }

        public static void Main()
        {
            //IEnumerableT_ExampleWithYield();
            //IComparableExamples();
            //IComparableT_Examples();


            IComparableCustom k1 = new IComparableCustom();
            k1.WorkMethod();
            
        }
        
    }

    static class Extension
    {
        public static IEnumerable<int> To(this int value, int maxValue)
        {
            for (int i = value; i <= maxValue; i++)
                yield return i;
        }
    }
}
