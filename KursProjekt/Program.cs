using KursProjekt.R10;
using KursProjekt.R11;
using KursProjekt.R9;
using KursProjekt.R9.InterfejsWbudawanyNET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjekt 
{
    class Program
    {

        public static void Main()
        {
            // R9
            ExampleInterface R9 = new ExampleInterface();
            //R9.WorkMethod();

            ExampleGenericInterface R10 = new ExampleGenericInterface();
            //R10.WorkMethod();

            ExampleDelegateEventLambda R11 = new ExampleDelegateEventLambda();
            R11.WorkMethod();



        }
        
    }

}
