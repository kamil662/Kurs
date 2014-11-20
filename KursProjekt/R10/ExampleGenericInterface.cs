using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KursProjekt.R10.GenericClass;
using KursProjekt.R10.GenericInterface;

namespace KursProjekt.R10
{
    class ExampleGenericInterface 
    {
        public void WorkMethod()
        {
            IComparableCustom_T klasaIComparable_T = new IComparableCustom_T();
            //klasaIComparable_T.WorkMethod();

            // Wbudowane generyczne klasy z przestrzeni System.Collecion.Generic
            GenericCollectionsClass klasaGenericCollectionsClass = new GenericCollectionsClass();
            //klasaGenericCollectionsClass.WorkMethod();

            IEnumerableT_ExampleWithYield klasaIEnumerableT_ExampleWithYield = new IEnumerableT_ExampleWithYield();
            //klasaIEnumerableT_ExampleWithYield.WorkMethod();

            //Generyczna prosta struktura 
            GenericClass.GenericClass klasaGenericStrukture = new GenericClass.GenericClass();
            klasaGenericStrukture.WorkMethod();

            // Generyczna klasa jako Drzewo Binarne, dla przechowywania dowolnego typu danych.
            BinaryTreeGeneric klasaBinaryTreeGeneric = new BinaryTreeGeneric();
            klasaBinaryTreeGeneric.WorkMethod();

            // Implementacja Generycznych interfejsów - Kowariancja oraz Kontrawariancja 
            GenericInterface.GenericInterface klasaGenericInterface = new GenericInterface.GenericInterface();
            //klasaGenericInterface.WorkMethod();

        }
    }
}
