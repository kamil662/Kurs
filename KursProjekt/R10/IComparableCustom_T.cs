using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Example:
 * http://msdn.microsoft.com/pl-pl/library/234b841s(v=vs.110).aspx
 * 
 * Parametr dla metody ComapreTo musi być zgodny z typem określonym w interfejsie IComparable<T>
 * Więc nie trzeba sprawdzać, czy wejściowym parametrem jest Car, ponieważ innego być nie może! 
 * Jak ktoś poda niekompatybilny typ danych, otrzyma błąd kompilacji.
 */


namespace KursProjekt.R10
{
    class IComparableCustom_T
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
                Console.WriteLine("tablicaInt[" + (i + 1) + "] = " + tablicaInt[i]);

            for (int i = 0; i < 5; i++)
                Console.WriteLine("tablicaString[" + (i + 1) + "] = " + tablicaString[i]);



            //Wbudowana statyczna metoda sortuje Inty;
            Array.Sort<int>(tablicaInt);
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
            Array.Sort<MyNewCar>(myAutos);

            // Display sorted array.
            Console.WriteLine("Here is the ordered set of cars:");
            foreach (MyNewCar c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.Petname);
            Console.WriteLine();

            // Now sort by CurrSpeed.
            Array.Sort<MyNewCar>(myAutos, MyNewCar.SortByCurrentSpeed);

            // Dump sorted array.
            Console.WriteLine("Ordering by CurrentSpeed:");
            foreach (MyNewCar c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.CurrSpeed);

            Console.ReadLine();
        }
    }



    public class MyNewCar : IComparable<MyNewCar>
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

        #region IComparable Members

        //wykorzystanie metody wbudowanego obiektu string - odpowiedzialność za definicje sortowania przerzucam na klase String
        // zakomentowany kod to różnica w implementacji metody Interfejsu IComparable niegenerycznego!
        public int CompareTo(MyNewCar obj)
        {
            //MyNewCar tmp = obj as MyNewCar;
            //if (tmp != null) {
                return this.Petname.CompareTo(obj.Petname);
            //}
            //else {
            //    throw new ArgumentException("Parameter is not a Car!"); }
        }

        // Możliwe wykorzystanie tylko po rzutowaniu obiektu na IComparable;
        // Podstawowa definicja porównania - tu względem CarID - typu INT (można tak samo jak wyżej)
        int IComparable<MyNewCar>.CompareTo(MyNewCar obj)
        {
            //MyNewCar temp = obj as MyNewCar;
            //if (temp != null)
            //{
            if (this.CarID > obj.CarID)
                return 1;
            if (this.CarID < obj.CarID)
                return -1;
            else
                return 0;

            //} else
            //    throw new ArgumentException("Parameter is not a Car!");
        }

        // Właściwość zwracająca obiekt definiujący porównanie 
        public static IComparer<MyNewCar> SortByCurrentSpeed
        {
            get
            {
                return (IComparer<MyNewCar>)new ComparerClass();
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("CarID: {0}, CarName: {1}, CarSpeed: {2}", CarID, Petname, CurrSpeed);
        }
    }

    //Sortowanie pod względem wartości CurrentSpeed
    public class ComparerClass : IComparer<MyNewCar>
    {
        // Metoda niegenerycznego interfejsu IComparer - argumenty typu OBJECT
        public int Compare(MyNewCar x, MyNewCar y)
        {
            //MyNewCar t1 = x as MyNewCar;
            //MyNewCar t2 = y as MyNewCar;
            //if (t1 != null && t2 != null) {

            return x.CurrSpeed.CompareTo(y.CurrSpeed);

            //} else throw new ArgumentException("Parametr is no Car!");
        }
    }


}
