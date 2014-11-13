using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Interfejs IComparable wymusza zdefiniowana metody int CompareTo(objecto) - w niej porównywane są 2 obiekty 
 * Pozwala obiektowi określić relację względem innyc obiektów
 * 
 * Metoda CompareTo() zwraca wartości mówiące o wyniku porównywania 2 obiektów
 *  - liczba < 0 - ta instancja (this.) jest mniesza od podanego obiektu
 *  -       0    - ta instancja (this.) jest równa podanemu obiektowi
 *  - liczba > 0 - ta instancja (this.) jest większa od podanego obiektu
 * 
 * Dla możliwości porównania obiektu pod różnymi względami (różne zmienne), 
 * stworzono nową klasę, którą podaje się w konstruktorze tworzonego obiektu.
 * IComparer - jest interfejsem który pozwala sortować za pomocą kilku kryteriów sortowania
 * Tworzony zostaje obiekt który implementuje IComparer (i definiuje jak mają być porównywane obiekty), 
 * który podawany jest później do metody Array.Sort() jako drugi argument typu IComparer
 * - W takiej sytuacji Kryteriów sortowania można mieć tyle ile zdefiniowanych takih klas.
 * */

namespace KursProjekt.R9.InterfejsWbudawanyNET
{
    class IComparableCustom
    { 
        
        public void WorkMethod()
        {
            SortowanieZwyklychTypow();
            sortujWlasneObiekty();
        }        
        
        #region Przykład sortowania tablic Wbudowanych typów

        private void SortowanieZwyklychTypow()
        {
            Console.WriteLine("Korzystanie z wbudowanych definicji Sortowania tablic Wbudowanych typów INT i String");

            int[] tablicaInt = new int[5] { 3, 10, 90, 0, 33 };
            string[] tablicaString = new string[5] { "ww", "aa", "pp", "ee", "hh" };


            Console.WriteLine("Tablica int/string nieposortowana:");

            for (int i = 0; i < 5; i++)
                Console.WriteLine("tablicaInt["+(i+1)+"] = " + tablicaInt[i]);
            
            for (int i = 0; i < 5; i++)
                Console.WriteLine("tablicaString[" + (i + 1) + "] = " + tablicaString[i]);



            //Wbudowana statyczna metoda sortuje Inty;
            Array.Sort(tablicaInt);
            // Odwraca kolejność elementów w tablicy
            Array.Reverse(tablicaInt);

            // Użycie LINQ do sortowania - tu Malejąco
            var sort = from s in tablicaInt
                       orderby s descending//Ascending
                       select s;
        }

        #endregion


        private void sortujWlasneObiekty()
        {
            Console.WriteLine("***** Fun with Object Sorting *****\n");
            // Make an array of Car types.
            MyNewCar[] myAutos = new MyNewCar[5];
            myAutos[0] = new MyNewCar("Rusty", 80, 1);
            myAutos[1] = new MyNewCar("Mary", 40, 234);
            myAutos[2] = new MyNewCar("Viper", 40, 34);
            myAutos[3] = new MyNewCar("Mel", 40, 4);
            myAutos[4] = new MyNewCar("Chucky", 40, 5);

            // Display current array.
            Console.WriteLine("Here is the unordered set of cars:");
            foreach (MyNewCar c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.Petname);
            Console.WriteLine();

            // Now, sort them using IComparable!
            Array.Sort(myAutos);

            // Display sorted array.
            Console.WriteLine("Here is the ordered set of cars:");
            foreach (MyNewCar c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.Petname);
            Console.WriteLine();

            // Now sort by CurrSpeed.
            Array.Sort(myAutos, MyNewCar.SortByCurrentSpeed);

            // Dump sorted array.
            Console.WriteLine("Ordering by CurrentSpeed:");
            foreach (MyNewCar c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.CurrSpeed);

            Console.ReadLine();
        }

    }



    public class MyNewCar : IComparable
    {
        // Constant for maximum speed.
        public const int maxSpeed = 100;

        // Car properties.
        public int CurrSpeed { get; set; }
        public string Petname { get; set; }
        public int CarID { get; set; }

        // Is the car still operational?
        private bool carisDead;

        // Constructors.
        public MyNewCar() { }
        public MyNewCar(string name, int speed, int id)
        {
            CurrSpeed = speed;
            Petname = name;
            CarID = id;
        }

        public override string ToString()
        {
            return string.Format("CarID: {0}, CarName: {1}, CarSpeed: {2}", CarID, Petname, CurrSpeed);
        }

        #region Accelerate w/ exception.
        // See if Car has overheated.
        public void Accelerate(int delta)
        {
            if (carisDead)
                Console.WriteLine("{0} is out of order...", Petname);
            else
            {
                CurrSpeed += delta;
                if (CurrSpeed >= maxSpeed)
                {
                    carisDead = true;
                    CurrSpeed = 0;

                    // We need to call the HelpLink property, thus we need
                    // to create a local variable before throwing the Exception object.
                    Exception ex =
                      new Exception(string.Format("{0} has overheated!", Petname));
                    ex.HelpLink = "http://www.CarsRUs.com";

                    // Stuff in custom data regarding the error.
                    ex.Data.Add("TimeStamp",
                      string.Format("The car exploded at {0}", DateTime.Now));
                    ex.Data.Add("Cause", "You have a lead foot.");
                    throw ex;
                }
                else
                    Console.WriteLine("=> CurrentSpeed = {0}", CurrSpeed);
            }
        }
        #endregion

        #region IComparable Members

        //wykorzystanie metody wbudowanego obiektu string - odpowiedzialność za definicje sortowania przerzucam na klase String
        public int CompareTo(object obj)
        {
            MyNewCar tmp = obj as MyNewCar;
            if (tmp != null)
            {
                return this.Petname.CompareTo(tmp.Petname);
            }
            else
            {
                throw new ArgumentException("Parameter is not a Car!");
            }
        }

        // Możliwe wykorzystanie tylko po rzutowaniu obiektu na IComparable;
        // Podstawowa definicja porównania - tu względem CarID - typu INT (można tak samo jak wyżej)
        int IComparable.CompareTo(object obj)
        {
            MyNewCar temp = obj as MyNewCar;
            if (temp != null)
            {
                if (this.CarID > temp.CarID)
                    return 1;
                if (this.CarID < temp.CarID)
                    return -1;
                else
                    return 0;
            }
            else
                throw new ArgumentException("Parameter is not a Car!");
        }

        // Właściwość zwracająca obiekt definiujący porównanie 
        public static IComparer SortByCurrentSpeed
        { 
            get 
            { 
                return (IComparer)new ComparerClass(); 
            } 
        }

        #endregion
    }

    //Sortowanie pod względem wartości CurrentSpeed
    public class ComparerClass : IComparer
    {
        public int Compare(object x, object y)
        {
            MyNewCar t1 = x as MyNewCar;
            MyNewCar t2 = y as MyNewCar;

            if (t1 != null && t2 != null)
            {
                return t1.CurrSpeed.CompareTo(t2.CurrSpeed);
            }
            else throw new ArgumentException("Parametr is no Car!");
        }
    }
}
