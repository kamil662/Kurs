using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Interfejs IEnumerable wymusza implementację metody GetEnumerator(), która zwraca obiekt typu IEnumerator;
 * Metoda GetEnumerator() zwraca referencję do kolejnego interfejsu - IEnumerator;
 * 
 * Interfejs GetEnumerator() informuje procedurę wywołującą, że podelementy obiektu mogą być wyliczane;
 * IEnumerator narzuca aby obiekt implementował metody MoveNext(), Reset(), 
 * posiada również właściwość (tylko do odczytu) Current - przechowuje bieżący element
 * 
 * W tym przykładzie (chodź można pisać własne implementacje MoveNext() czy Reset() ) wykorzystano fakt,
 * że wbudowany typ System.Array już implementuje(!) interfejsy Ienumerable i Enumerator.
 * Obiekt typu IEnumerator jest uzyskany z klasy System.Array;
 * 
 * Jeśli potrzebujemy niestandardowej iteracji należy odpowiednio zaimplementować metodę MoveNext() w obiekcie implementującym
 * imterfejs IEnumerable - przykład w klasie IEnumeratorCustom 
 * */


namespace KursProjekt.R9.InterfejsWbudawanyNET
{
    class IEnumerableCustom
    {
        public void WorkMethod()
        {
            Console.WriteLine("***** Custom IEnumerable *****\n");
            Garage garaz = new Garage();

            // Hand over each car in the collection?
            foreach (NewCar c in garaz)
            {
                Console.WriteLine("{0} is going {1} MPH",
                  c.PetName, c.CurrentSpeed);
            }
            Console.WriteLine();


            // Ręczna iteracja po kolekcji - możliwa gdy metoda GetEnumerator() jest publiczna
            IEnumerator i = garaz.GetEnumerator();
            i.MoveNext();
            NewCar myCar = (NewCar)i.Current;
            Console.WriteLine("{0} is going {1} MPH", myCar.PetName, myCar.CurrentSpeed);

            Console.ReadLine();
        }
    }

    #region Klasa Implementuje IEnumerable

    /// <summary>
    /// Garaż - zawiera tablice obiektów Car
    /// Wypełniana jest w konstruktorze
    /// </summary>
    public class Garage : IEnumerable
    {
        private NewCar[] carArray = new NewCar[4];

        // Konstruktor wypełnia tablice Car
        public Garage()
        {
            carArray[0] = new NewCar("Rusty", 30);
            carArray[1] = new NewCar("Clunker", 55);
            carArray[2] = new NewCar("Zippy", 30);
            carArray[3] = new NewCar("Fred", 30);
        }

        // Przykład - implementacja publicznej metody narzuconej przez Interfejs 
        public IEnumerator GetEnumerator()
        {
            // Tu delegowane żądanie do System.Array 
            return carArray.GetEnumerator();
        }

        //Jawna implementacja metody - ukrywa funkcje IEnumerable na poziomie obiektu. Domyślnie prywatna
        IEnumerator IEnumerable.GetEnumerator()
        {
            // Tu delegowane żądanie do System.Array 
            return carArray.GetEnumerator();
        }

    }

    #endregion

    #region Obiekt Car
    //Niestandardowy obiekt : nie implementuje interfejsów
    public class NewCar
    {
        // Constant for maximum speed.
        public const int MaxSpeed = 100;

        // Car properties.
        public int CurrentSpeed { get; set; }
        public string PetName { get; set; }

        // Is the car still operational?
        private bool carIsDead;

        // Constructors.
        public NewCar() { }
        public NewCar(string name, int speed)
        {
            CurrentSpeed = speed;
            PetName = name;
        }
    }
    #endregion

}
