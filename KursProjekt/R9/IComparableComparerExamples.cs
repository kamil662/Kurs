using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/***
 * Implementacja IComparable polega na zdefiniowaniu metody "int CompareTo(object obj)" - w niej porównywane są 2 obiekty 
 * i zwraca 1, -1, bądź 0 dla równych. Dla wbudowanych obiektów można wykorzystać ich własną metodę CompareTo() (w przykładzie string).
 * 
 * Dla możliwości porównania obiektu pod różnymi względami (różne zmienne), 
 * stworzono nową klasę, którą podaje się w konstruktorze tworzonego obiektu.
 */

namespace KursProjekt.R9
{
    public class Car : IComparable
    {
        // Constant for maximum speed.
        public const int MaxSpeed = 100;

        // Car properties.
        public int CurrentSpeed { get; set; }
        public string PetName { get; set; }
        public int CarID { get; set; }

        // Is the car still operational?
        private bool carIsDead;

        // Constructors.
        public Car() { }
        public Car(string name, int speed,int id)
        {
            CurrentSpeed = speed;
            PetName = name;
            CarID = id;
        }

        public override string ToString()
        {
            return string.Format("CarID: {0}, CarName: {1}, CarSpeed: {2}", CarID, PetName, CurrentSpeed);
        }

        #region Accelerate w/ exception.
        // See if Car has overheated.
        public void Accelerate(int delta)
        {
            if (carIsDead)
                Console.WriteLine("{0} is out of order...", PetName);
            else
            {
                CurrentSpeed += delta;
                if (CurrentSpeed >= MaxSpeed)
                {
                    carIsDead = true;
                    CurrentSpeed = 0;

                    // We need to call the HelpLink property, thus we need
                    // to create a local variable before throwing the Exception object.
                    Exception ex =
                      new Exception(string.Format("{0} has overheated!", PetName));
                    ex.HelpLink = "http://www.CarsRUs.com";

                    // Stuff in custom data regarding the error.
                    ex.Data.Add("TimeStamp",
                      string.Format("The car exploded at {0}", DateTime.Now));
                    ex.Data.Add("Cause", "You have a lead foot.");
                    throw ex;
                }
                else
                    Console.WriteLine("=> CurrentSpeed = {0}", CurrentSpeed);
            }
        }
        #endregion

        #region IComparable Members

        //wykorzystanie metody wbudowanego obiektu string
        public int CompareTo(object obj)
        {
            Car tmp = obj as Car;
            if (tmp != null)
            {
                return this.PetName.CompareTo(tmp.PetName);
            }
            else
            {
                throw new ArgumentException("Parameter is not a Car!");
            }
        }

        //możliwe wykorzystanie tylko po rzutowaniu obiektu na IComparable
        int IComparable.CompareTo(object obj)
        {
            Car temp = obj as Car;
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

        public static IComparer SortByCurrentSpeed
        { get { return (IComparer)new ComparerClass(); } }

        #endregion
    }

    //Sortowanie pod względem wartości CurrentSpeed
    public class ComparerClass : IComparer
    {
        public int Compare(object x, object y)
        {
            Car t1 = x as Car;
            Car t2 = y as Car;

            if (t1 != null && t2 != null)
            {
                return t1.CurrentSpeed.CompareTo(t2.CurrentSpeed);
            }
            else throw new ArgumentException("Parametr is no Car!");
        }
    }

}
