using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjekt.R9.InterfejsNiestandardowy
{
    /**     OGÓLNIE
     * 1. Dla Interejsów nie podaje się klasy bazowej (nawet System.Object), ale Interfejs może mieć bazowy Interfejs;
     * 2. Interfejsy mogą dziedziczyć tylko z innych interfejsów, ale może to być więcej niż jeden
     * 3. Składowe interfejsu mie mają (i mieć nie mogą) modyfikatorów dostępu - domyślnie wszystkie są pulic abstract;
     * 4. Interfejsy mogą przechowywać definicję metody, właściwości zdarzeń i indeksatorów;
     * */



    class Example
    {
        public void WorkMethod()
        {
            //proste przypadki użycia Interfejsów niestandardowych
            CustomInterface klasaCI = new CustomInterface();
            klasaCI.WorkMethod();
        }
    }
}
