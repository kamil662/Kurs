using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* W przypadku implementacji interfejsów, gdzie jest możliwy konflikt nazw, nie wymagana jest jawna implementacja 
 * Jak w tym przykładzie klasa Rectangle Implementuje tylko RAZ metodę Draw(), oraz oczywiście pozostałe składowe Interfejsów
 * 
 * Hierarchie Interfejsów są przydatne gdy chcemy rozbudować zbiór funcji istniejącego interfejsu, 
 * nie naruszając istniejących baz kodu.
 * */

namespace KursProjekt.R9.InterfejsNiestandardowy
{
    class InterfaceHierarchy
    {
        public void WorkMethod()
        {
            Square square = new Square();
            // Metody interfejsu IPrintable są widoczne 
            square.GetNumberOfSides();
            square.Print();
            //square.Draw   <- nie widoczne z poziomu obiektu - trzeba rzutować
            IDrawable obiektIdrawable = square as IDrawable;
            obiektIdrawable.Draw();//   <- teraz OK.

            // Metoda Draw() jest publiczna i dlatego widoczna z poziomu obiektu
            Rectangle rectangle = new Rectangle();
            rectangle.GetNumberOfSides();
            rectangle.Print();
            rectangle.Draw();
        }
    }

    #region Interfejs Hierarchy

    public interface IDrawable
    {
        void Draw();
    }

    interface IPrintable
    {
        void Print();
        void Draw(); // <-- Możliwy konflikt nazw
    }

    // Wielokrotne dziedziczenie Interfejsów jest ok
    interface IShape : IDrawable, IPrintable
    {
        int GetNumberOfSides();
    }

    public interface IAdvancedDraw : IDrawable
    {
        void DrawInBoundingBox(int top, int left, int bottom, int right);
        void DrawUpsideDown();
    }

    #endregion

    #region Obiekty implementujące Interfejsy

    public class Square : IShape
    {
        // Używając jawnej implementacji (explicit) rozwiązywany jest konflikt nazw
        void IPrintable.Draw()
        { // Draw to printer ...
        }

        void IDrawable.Draw()
        { // Draw to screen ...
        }

        public void Print()
        { // Print ...
        }

        public int GetNumberOfSides()
        { return 4; }
    }


    // Klasa Implementuje Interfejs IShape z tylko jedną implementacją metody Draw()
    public class Rectangle : IShape
    {
        public int GetNumberOfSides()
        { return 4; }

        public void Draw()
        { Console.WriteLine("Drawing..."); }

        public void Print()
        { Console.WriteLine("Prining..."); }
    }

    #endregion
}
