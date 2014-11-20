using KursProjekt.R11.Delegate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjekt.R11
{
    class ExampleDelegateEventLambda
    {
        public void WorkMethod()
        {
            // Przykład wykorzystania prostego delegaty i kowariancja delegat
            CarDelegate klasaCarDelegate = new CarDelegate();
            klasaCarDelegate.WorkMethod();

        }
    }
}
