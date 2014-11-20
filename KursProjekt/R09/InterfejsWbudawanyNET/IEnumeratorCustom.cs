using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***
 * http://msdn.microsoft.com/pl-pl/library/system.collections.ienumerator(v=vs.110).aspx
 * Przykład wykorzystania/implementacji interfejsu Ienumerator z metodami MoveNext, Reset, właściwością Current
 * dla iterowania tablicy obiektów Person
 * 
 * inny przykład: http://www.codeproject.com/Articles/474678/A-Beginners-Tutorial-on-Implementing-IEnumerable-I
 * IEnumerable musi najpierw zaimplementować interfejs IEnumerable w swojej kolekcji ,
 * a potem zaimplementować interfejs IEnumerator tak aby on mógłby być zwracany w metodzie GetEnumerator() w klasie kolekcji. 
 * 
 * Zwracany przez metodę GetEnumerator obiekt, można nazwać wskaźnikiem na kolejny element z listy;
 * Tutaj zdefiniowano własny obiekt implementujący IEnumerator, metody MoveNext(), Reset() i właściwość Current.
 * */


/**     OPIS DZIAŁANIA PĘTLI FOREACH() ********
 * 
 * W pętli foreach(var Item IN collecion) {} - Wykonują się kolejno kroki z użyciem tych sładowych (od końca):
 * - COLLECTION: Pobierany jest obiekt IEnumerator z metody GetEmunerator(), z obiektu collection (wykonywany jest tylko raz, na początku pętli)
 * - IN        : Wykonywana jest metoda MoveNext(), do pierwszego elementu kolekcji, jeśli jest zwraca true;
 * - ITEM      : Dla zwróconego true, do ITEM przypisywany jest obiekt z właściwości Current;
 * - IN        : ponownie metoda MoveNext() - jeśli zwróci false (nie ma więcej elementów w kolekcji) - wykonywana jest metoda Reset(), a pętka przerywana; */


namespace KursProjekt.R9.InterfejsWbudawanyNET
{
    public class IEnumeratorCustom
    {
        public void WorkMethod()
        {

            People peopleList = new People();

            // korzytanie z iteratora publicznej metody - czyli zdefiniwanego w klasie PeopleEnum()
            foreach (Person p in peopleList)
            {
                Console.WriteLine(p.firstName + " " + p.lastName);
            }

            // Iterator z metody prywatnej - czyli z System.Array
            IEnumerator i = peopleList.GetEnumerator();
            i.MoveNext();
            Person myPerson = (Person)i.Current;
            Console.WriteLine("{0} is going {1} MPH", myPerson.firstName, myPerson.lastName);

        }
    }


    #region Klasa Implementuje IEnumerable

    //Metoda GetEnumerator() zwraca nowy obiekt zdefiniowany, który iteruje po kolejcji
    public class People : IEnumerable
    {
        private Person[] _people = new Person[5];
        public People()
        {
            _people[0] = new Person("John", "Smith");
            _people[1] = new Person("Jim", "Johnson");
            _people[2] = new Person("Sindy", "Rabon");
            _people[3] = new Person("Sue", "McDonald");
            _people[4] = new Person("Tom", "McRabon");
        }

        //pobiera wyliczenie z System.Array
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        //Własne zdefiniowany iterator
        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }
    }

    #endregion

    #region Klasa implementuje IEnumerator - sposób iterowania po kolekcji

    public class PeopleEnum : IEnumerator
    {
        public Person[] _people;

        // Enumerator umieszczany jest przez pierwszym elementem, przed pierwszym wywołaniem MoveNext()
        // Ma to sens gdyż kolekcja może być pusta
        int position = -1;

        //uzyskuje referencje do kolekcji
        public PeopleEnum(Person[] list)
        {
            _people = list;
        }

        //przesuwam wskaźnik, i zwracam false jeśli nie ma więcej obiektów w kolekcji
        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        // Zwraca obiekt kolekcji z konkretnej pozycji 
        // Ważne - Właściwość jest typu OBJECT, wymusza to rzutowanie na konkretny typ;
        // nie ma tego problemu w interfejsie IEnumerator<E>
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Person Current
        {
            get
            {
                try
                {
                    return _people[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    #endregion

    #region Obiekt Person

    public class Person
    {
        public string firstName;
        public string lastName;

        public Person(string fName, string lName)
        {
            this.firstName = fName;
            this.lastName = lName;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} {1}", firstName, lastName);
        }
    }

    #endregion
}
