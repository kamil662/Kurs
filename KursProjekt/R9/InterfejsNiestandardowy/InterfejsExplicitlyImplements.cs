using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Gdy klasa implementuje kilka interfejsów, zawsze istnieje możliwość że będą zawierać identyczne składowe;
 * Aby rozwiązać konflikt nazw (występujący w takiej sytuacji) należy zastosować jawną implementacje: Explicitly;
 * 
 * W takim wypadku składowe te stają się automatycznie prywatne (tu metoda Draw() ); 
 * Aby jednak dostać się do składowych należy obiekt rzutować na Imterfejs;
 */

namespace KursProjekt.R9.InterfejsNiestandardowy
{

    class InterfejsExplicitlyImplements
    {
        public void WorkMethod()
        {
            Console.WriteLine("***** Jawna Implementacja *****\n");
            Octagon oct = new Octagon();

            //oct.Draw();   <- metoda nie widoczna z poziomu obiektu!

            // rzutowanie obiektu na Interfejs - daje to dostęp do pożądanych metod
            IDrawToForm itfForm = (IDrawToForm)oct;
            itfForm.Draw();

            // skrócony zapis, jeśli zmienna interfejsu nie będzie potrzebna
            ((IDrawToPrinter)oct).Draw();

            // Could also use the "as" keyword.
            if (oct is IDrawToMemory)
                ((IDrawToMemory)oct).Draw();

            Console.ReadLine();
        }
    }

    #region Interfejsy mające tą samą metodę

    // Rysuj obrazek w oknie programu
    public interface IDrawToForm
    {
        void Draw();
    }

    // Rysuj/wstaw obrazek do bufora pamięci
    public interface IDrawToMemory
    {
        void Draw();
    }

    // Rysuj/Drukuj obrazek na drukarce
    public interface IDrawToPrinter
    {
        void Draw();
    }

    #endregion

    class Octagon : IDrawToForm, IDrawToMemory, IDrawToPrinter
    {
        // Jawnie dowiązujemy implementacje metody Draw() 
        // do konkretnego interfejsu
        void IDrawToForm.Draw()
        {
            Console.WriteLine("Drawing to form...");
        }
        void IDrawToMemory.Draw()
        {
            Console.WriteLine("Drawing to memory...");
        }
        void IDrawToPrinter.Draw()
        {
            Console.WriteLine("Drawing to a printer...");
        }
    }

}
