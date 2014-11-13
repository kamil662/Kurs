using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/***
 * Tutorial:
 * http://mndevnotes.wordpress.com/2012/08/18/instrukcja-yield-return-tworzenie-leniwych-kolekcji-danych/
 * http://adventuresdotnet.blogspot.com/2010/06/understanding-cs-yield-keyword-and.html
 * 
        // Używanie słowa kluczowego YIELD wymaga aby :
        //  - metoda zwracała typ IEnumerator,
        //  - metoda była wywoływana w wyrażeniu iteracyjnym (for/foreach); metoda ta nie może być anonimowa
        //  - nie może znajdować się w sekcji try jeżeli po niej występuje sekcja catch;
        //  - jeżeli chcemy przerwać zwracanie kolejnych elementów kolekcji używamy polecenia yield break;
 * 
 * Użycie słowa kluczowego yield w celu zdefiniowania iteratora eliminuje konieczność jawnego użycia dodatkowej klasy więc
 * klasa Car nie wymaga implementacji interfejsu IEnumerator, czy metod MoveNext()
 * 
 * - Instrukcja yield return umożliwia jednokrotne zwrócenie każdego elementu.
 */

namespace KursProjekt.R9.InterfejsWbudawanyNET
{

    public class IEnumerableWithYield
    {
        public void WorkMethod()
        {
            NewGarage newGaraz = new NewGarage();
            foreach (NewCar car in newGaraz)
            {
                Console.WriteLine(car.ToString());
            }
            Console.WriteLine("Metoda Iteracyna zwraca w odrotnej kolejności Auta:" + Environment.NewLine);
            foreach (NewCar car in newGaraz.GetTheCars(true) )
            {
                Console.WriteLine(car.ToString());
            }

            /******************************************************************************************************/

            // Lets see how yield return works
            foreach (int i in YieldReturn())
            {
                Console.WriteLine(i);
            }


            Console.WriteLine("Praktyczny przykład Metody Iteracyjnej - jednocześnie Extension\n2 do 8 - kolejne wyniki");
            foreach (int i in Extension.PodniesDoPotegi(2, 8))
            {
                Console.Write("{0} ", i);
            }
        }

        #region Metoda Iteracyja

        //Przykład innego użycia Yield - obrazuje jak działa
        static IEnumerable YieldReturn()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        #endregion
    }

    #region NewGaraz

    public class NewGarage : IEnumerable
    {
        private NewCar[] carTab;

        public NewGarage()
        {
            carTab[0] = new NewCar("as", 35);
            carTab[1] = new NewCar("asasd", 15);
            carTab[2] = new NewCar("asasffd", 25);
            carTab[3] = new NewCar("asfggs", 85);
        }


        //tzw. Metoda Iteracyjna
        public IEnumerator GetEnumerator()
        {
            foreach (NewCar c in carTab)
            {
                yield return c;
            }
        }

        //tzw. Metoda Iteracyjna
        public IEnumerable GetTheCars(bool ReturnRevesed)
        {
            // Zwraca obiekty w odwrotnej kolejności
            if (ReturnRevesed)
            {
                for (int i = carTab.Length; i != 0; i--)
                {
                    yield return carTab[i - 1];
                }
            }
            else
            {
                // Zwraca obiekty w kolejności przechowywanej w tablicy
                foreach (NewCar c in carTab)
                {
                    yield return c;
                }
            }
        }
    }

    #endregion

    #region Statyczna Metoda Iteracyjna jako Extension

    public static class Extension
    {
        /* W poniższym przykładzie przedstawiono instrukcję yield return, która znajduje się wewnątrz pętli for. 
         * Każda iteracja treści instrukcji foreach w Process tworzy wywołanie funkcji iteratora PodniesDoPotegi. 
         * Każde wywołanie funkcji iteratora powoduje przejście do następnego wykonania instrukcji yield return, 
         * do którego dochodzi podczas następnej iteracji pętli for.
         * 
         * Zwracany typ metody iteratora to IEnumerable, który jest typem interfejsu iteratora. Kiedy metoda iteratora 
         * jest wywoływana, zwraca obiekt wyliczeniowy, który zawiera potęgi liczb.
         * 
         * przykład z : http://msdn.microsoft.com/pl-pl/library/9k7k7cf0.aspx
         */
        public static IEnumerable<int> PodniesDoPotegi(this int wartosc, int potega)
        {
            int result = 1;
            for (int i = 0; i < potega; i++)
            {
                result = result * wartosc;
                yield return result;
            }
        }
    }

    #endregion

}
