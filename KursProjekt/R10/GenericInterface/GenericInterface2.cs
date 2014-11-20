using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Example:
 * http://4programmers.net/C_sharp/Kowariancja_i_kontrawariancja
 * 
 * Kowariancja i Kontrwariancja rozwiązuje problem który występuje w zasadzie tylko w językach silnie typowanych (C#);
 * 
 * Kowariancja - relacja mówiąca że relacja ta (kowariancja) jest możliwa tylko z danymi wyjściowymi, (typ danych które "wychodzą" trafiają do typu bardziej ogólnego ( Animal animal = Cat; )
 *               (a kompilator nie wie jak używamy typu generycznego); 
 *  < OUT >      Teraz można przyrównać obiekt typu A (Cat) do obiektu typu B (Animal), 
 *               jeśli  istnieje niejawna konwersja pomiędzy typem A i B, bądź typ A (Cat) jest pochodny od B (Animal);
 *              
 * Kontrwariancja - analogicznie : relacja jest możliwa z danymi wejściowymi; 
 *                  metody w generycznym interfejsie pobierają parametry jako obiekty,  to mogą też przyjmować napisy string jako parametry.
 * 
 * 
 * 
 * 
 * */

namespace KursProjekt.R10.GenericInterface
{

    // OUT - jest informacją dla kompilatora że w interfejsie chcemy tu użyć relacji Kowariancji
    // 
    interface ICovariance<out T>
    {
        T Get();
    }

    interface IContravariance<in T>
    {
        void Set(T obj);
    }


    class Tester<T> : ICovariance<T>, IContravariance<T>
    {
        public T Get()
        {
            return default(T);
        }

        public void Set(T obj)
        {
            
        }
    }

    /*---------    ---------     ---------    ---------     ---------     ---------       --*/

    // Zwykłe przykładowe klasy które dziedziczą po sobie
    public class Organizm
    {

    }

    public class Animal : Organizm
    {

    }

    public class Cat : Animal
    {

    }

}
