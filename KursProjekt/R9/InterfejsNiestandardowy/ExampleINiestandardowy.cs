using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**     OGÓLNIE
 * 1. Dla Interejsów nie podaje się klasy bazowej (nawet System.Object), ale Interfejs może mieć bazowy Interfejs;
 * 2. Interfejsy mogą dziedziczyć tylko z innych interfejsów, ale może to być więcej niż jeden
 * 3. Składowe interfejsu mie mają (i mieć nie mogą) modyfikatorów dostępu - domyślnie wszystkie są pulic abstract;
 * 4. Interfejsy mogą przechowywać definicję metody, właściwości zdarzeń i indeksatorów;
 * 
*/

namespace KursProjekt.R9.InterfejsNiestandardowy
{
    class ExampleINiestandardowy
    {
        public void WorkMethod()
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
    }
}
