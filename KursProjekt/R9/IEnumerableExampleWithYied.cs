using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/***
 * http://mndevnotes.wordpress.com/2012/08/18/instrukcja-yield-return-tworzenie-leniwych-kolekcji-danych/
 * 
 * Tutorial:
 * http://adventuresdotnet.blogspot.com/2010/06/understanding-cs-yield-keyword-and.html
 * 
        //używanie słowa kluczowego YIELD wymaga aby :
        // - metoda zwracała typ IEnumerator,
        // - metoda była wywoływana w wyrażeniu iteracyjnym (for/foreach)
 * 
 * W tym przykładzie klasa Car nie wymaga implementacji interfejsu IEnumerator, czy metod MoveNext()
 * 
 */

namespace KursProjekt.R9
{
    public class Garage : IEnumerable
    {
        private Car[] carTab;
        private int index;

        public Garage()
        {
            carTab = new Car[4];
            index = 0;
        }

        public void Add(Car c)
        {
            if (index >= carTab.Length)
            {
                Console.WriteLine("tablica jest pełna! Lenth = {0}",carTab.Length);
            }
            else
            {
                carTab[index] = c;
                index++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Car c in carTab)
            {
                if (c == null)
                {
                    Console.WriteLine("tablica jest pusta");
                    break;
                }
                yield return c;
            }
        }

    }

}

/*** inny przykład użycia YIELD 
 */
    //static IEnumerable<int> YieldReturn()
    //{
    //    yield return 1;
    //    yield return 2;
    //    yield return 3;
    //}
    //static void Main(string[] args)
    //{
    //    // Lets see how yield return works
    //    foreach (int i in YieldReturn())
    //    {
    //        Console.WriteLine(i);
    //    }
    //}