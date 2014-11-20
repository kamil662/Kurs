using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjekt.R10.GenericInterface
{
    public class GenericInterface
    {
        public void WorkMethod()
        {
            // Tworze obiekt 
            Informer<int> InfoInt = new Informer<int>();

            // Rzutowanie na generyczny interfejs - korzystanie z metod wymusza rzutowanie (jawna implementacja metod w klasie)
            ISetInfo<int> ISetInfoInt = InfoInt;
            ISetInfoInt.SetInfo(102);

            IGetInfo<int> IGetInfoInt = InfoInt;
            int tmp = IGetInfoInt.GetInfo();  // tmp = 102

            /*-------------------------------------------------------------------------------------*/

            // Kowariancja (out) - relacja z typu szczegółowego do ogólnego 
            // (przykład : object = string; LUB Figura = Trójkąt; LUB Organizm = Animal)
            ICovariance<Animal> animalTester = new Tester<Animal>();
            ICovariance<Organizm> organismTester = animalTester;

            // Kontrawariancja (IN) - relacja z typu ogólnego do szczegółowego (odwrotnie)
            IContravariance<Animal> animalTester2 = new Tester<Animal>(); //ok
            IContravariance<Cat> catTester = animalTester2; //ok

        }
    }
}
