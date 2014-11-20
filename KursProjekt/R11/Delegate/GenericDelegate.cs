using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Przykład pokazujący jak można zdefiniować delegata, który może wywołać dowolną metodę 
 * zwracającą void oraz przyjmującą jeden parametr. 
 * Jeśli argument ten może być dowolnego typu można zastosować parametr typu
 * 
 * W MyGenericDelegate<T> zdefiniowano jeden parametr typu, reprezentujący argument, który należy przesłać do celu
 * (wskazywanej przez deletata metody).
 * 
 * Symulacja generycznych delegacji bez generyczności z pokazaniem wad tego rozwiązania - 
 * kiedy wysyłamy typ wartościowy do Celu, wartość jest pakowana i wypakowywana po odebraniu przez metodę docelową.
 * Poza tym, ponieważ wejściowym parametrem może być cokolwiek, przed rzutowaniem trzeba dynamicznie sprawdzać bazowy typ.
 * */

namespace KursProjekt.R11.Delegate
{

    // Ten heneryczny delegat może wywołać dowolną metodę
    // zwracającą void i przyjmującą jeden parametr typu
    public delegate void MyGenericDelegate<T>(T arg);
    public delegate void MyDelegate(object arg);

    class GenericDelegate
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Generic Delegates *****\n");

            // Definicja Delegata z parametrem typu string
            MyGenericDelegate<string> strTarget = new MyGenericDelegate<string>(StringTarget);
            strTarget("Some string data");

            // Definicja Delegata z parametrem typu int
            MyGenericDelegate<int> intTarget = new MyGenericDelegate<int>(IntTarget);
            intTarget(9);

            //symulacja generycznych delegacji bez generyczności
            MyDelegate d = new MyDelegate(MyTarget);
            d("More string data");

            // Method group conversion syntax.
            MyDelegate d2 = MyTarget;
            d2(9);  // Boxing penalty.
            Console.ReadLine();
        }

        #region Targets for delegates
        static void StringTarget(string arg)
        {
            Console.WriteLine("String arg in uppercase is: {0}", arg.ToUpper());
        }

        static void IntTarget(int arg)
        {
            Console.WriteLine("Int ++arg is: {0}", ++arg);
        }

        // Due to a lack of type safety, we must
        // determine the underlying type before casting.
        static void MyTarget(object arg)
        {
            if (arg is int)
            {
                int i = (int)arg;  // Unboxing penalty.
                Console.WriteLine("++arg is: {0}", ++i);
            }

            if (arg is string)
            {
                string s = (string)arg;
                Console.WriteLine("arg in uppercase is: {0}", s.ToUpper());
            }
        }

        #endregion
    }
}
