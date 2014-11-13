using KursProjekt.R9.InterfejsWbudawanyNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * IEnumerable - aby kolekcja mogła być wyliczana za pomocą pętli foreach(), musi implementować IEmumerable i dodatkowo IEnumerator.
 * Oba interfejsy są implementacją WZORCA PROJEKTOWEGO - Iterator.
 * 
 * IEnumerable VS IEnumerator
 * IEnumerable - reprezentuje kolekcje, służy do pobrania instancji iteratora wspierającej interfejs IEnumerator;
 * IEnumerator - pozwala na iterację poszczególnych elementów listy, czy na pobranie aktualnego elementu;   ****/

namespace KursProjekt.R9
{
    class ExampleIWbudowany
    {
        public void WorkMethod()
        {
            //Najprostrza implementacja IEnumerable
            IEnumerableCustom klasaIEnumerableCustom = new IEnumerableCustom();
            klasaIEnumerableCustom.WorkMethod();

            // Metoda interfejsu IEnumerable zwraca specjalnie zdefiniowany obiekt iteracyjny typu IEnumerator
            IEnumeratorCustom klasaIEnumeratorCustom = new IEnumeratorCustom();
            klasaIEnumeratorCustom.WorkMethod();

            // Przykładowe użycia operatora YIELD w pętlach, wraz z IEnumerable
            IEnumerableWithYield klasaIEnumerableWithYield = new IEnumerableWithYield();
            klasaIEnumerableWithYield.WorkMethod();

        }
    }
}
