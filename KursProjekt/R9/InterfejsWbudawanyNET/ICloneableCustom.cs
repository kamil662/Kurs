using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
 * MemberwiseClone() - pozwala uzyskać płytką kopię obiektu - wszystkie typy wartościowe kopiuje poprawnie, 
 * obiekty (jako typy referencyjne) dostają kopie referencji - w efekcie wskazują na te same wartości.
 * */

namespace KursProjekt.R9.InterfejsWbudawanyNET
{
    class ICloneableCustom
    {
        public void WorkMethod()
        {
            Trojkat trojkat = new Trojkat();
            Console.WriteLine("Pierwszy trojkat: \n"+trojkat.ToString());
            Console.WriteLine("Tworze nowy obiekt kopiując z już istniejącego");

            // Wymuszone rzutowanie typu - gdyż metoda CLONE() zwraca typ object;
            // Interfejs ICloneable<T> nie wymusza rzutowania
            Trojkat kopiaTrojkat = trojkat.Clone() as Trojkat;

            if (kopiaTrojkat != null)
            {
                Console.WriteLine("Zmieniam w 1 obiekcie P1.x i Y na 102, 202");
                trojkat.P1.X = 102;
                trojkat.P1.Y = 202;

                Console.WriteLine("Drugi trojkat: \n" + kopiaTrojkat.ToString());
                Console.WriteLine("Pierwszy trojkat: \n" + trojkat.ToString());
            }
        }
    }

    #region Obiekty Implementujące ICloneable

    public class Trojkat : ICloneable
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public Point P3 { get; set; }

        public Trojkat()
        {
            this.P1 = new Point(1, 4);
            this.P2 = new Point(4, 6);
            this.P3 = new Point(10, 4);
        }

        public object Clone()
        {
            Trojkat tmp = new Trojkat();
            tmp.P1 = (Point)tmp.P1.Clone();
            tmp.P2 = (Point)tmp.P2.Clone();
            tmp.P3 = (Point)tmp.P3.Clone();
            return tmp;
        }

        public override string ToString()
        {
            return string.Format("P1 = {0}P2 = {1}P3 = {2}", P1, P2, P3);
        }
    }

    // A class named Point.
    public class Point : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PointDescription desc = new PointDescription();

        public Point(int xPos, int yPos, string petName)
        {
            X = xPos; Y = yPos;
            desc.PetName = petName;
        }
        public Point(int xPos, int yPos)
        {
            X = xPos; Y = yPos;
        }
        public Point() { }

        // Override Object.ToString().
        public override string ToString()
        {
            return string.Format("X = {0}; Y = {1};\n", X, Y);
        }

        // Return a copy of the current object.
        // Now we need to adjust for the PointDescription member.
        public object Clone()
        {
            // Uzyskanie płytkiej kopii
            Point newPoint = (Point)this.MemberwiseClone();

            // Tworzenie nowych podobiektów i przypisanie referencji
            PointDescription currentDesc = new PointDescription();
            currentDesc.PetName = this.desc.PetName;
            newPoint.desc = currentDesc;
            return newPoint;
        }

    }

    // Klasa opisująca punkt - ważne że to obiekt referencyjny
    public class PointDescription
    {
        public string PetName { get; set; }
        public Guid PointID { get; set; }

        public PointDescription()
        {
            PetName = "No-name";
            PointID = Guid.NewGuid();
        }
    }

    #endregion

}
