using KursProjekt.R9.InterfejsNiestandardowy;
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
    class ExampleInterface
    {

        public void WorkMethod()
        {

            ExampleInterfaceNiestandardowy();

            ExampleInterfaceWbudowany();
        }



        public void ExampleInterfaceNiestandardowy()
        {
            //proste przypadki użycia Interfejsów niestandardowych
            CustomInterface klasaCustomI = new CustomInterface();
            klasaCustomI.WorkMethod();

            //użycie jawnej implementacji interfejsów
            InterfejsExplicitlyImplements klasaIExplicitlyImpl = new InterfejsExplicitlyImplements();
            klasaIExplicitlyImpl.WorkMethod();

            //Hierarchia Interfejsów
            InterfaceHierarchy klasaIHierarchy = new InterfaceHierarchy();
            klasaIHierarchy.WorkMethod();
        }

        public void ExampleInterfaceWbudowany()
        {
            // Implementacja IClonable - głęboka kopia hierarchii obiektów
            ICloneableCustom klasaICloneable = new ICloneableCustom();
            klasaICloneable.WorkMethod();

            // Interfejs wymusza zdefiniowania metody która definiuje porównywanie obiektów,
            // można dzięki temu sortować obiekty wg niestandardowych danych;
            IComparableCustom klasaIComparable = new IComparableCustom();
            klasaIComparable.WorkMethod();

            //Najprostrza implementacja IEnumerable
            IEnumerableCustom klasaIEnumerable = new IEnumerableCustom();
            klasaIEnumerable.WorkMethod();

            // Przykładowe użycia operatora YIELD w pętlach, wraz z IEnumerable
            IEnumerableWithYield klasaIEnumerableWithYield = new IEnumerableWithYield();
            klasaIEnumerableWithYield.WorkMethod();

            // Metoda interfejsu IEnumerable zwraca specjalnie zdefiniowany obiekt iteracyjny typu IEnumerator
            IEnumeratorCustom klasaIEnumerator = new IEnumeratorCustom();
            klasaIEnumerator.WorkMethod();
        }
        
    }
}
