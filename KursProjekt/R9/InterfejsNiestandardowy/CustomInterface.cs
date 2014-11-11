using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjekt.R9.InterfejsNiestandardowy
{
    #region Interfejsy

    // This interface defines the behavior of "having points."
    public interface IPointy
    {
        // Implicitly public and abstract.
        // byte GetNumberOfPoints();

        byte Points { get; }
    }

    // Models the ability to render a type in stunning 3D.
    public interface IDraw3D
    {
        void Draw3D();
    }

    #endregion



    class CustomInterface
    {
        public void WorkMethod()
        {
            Console.WriteLine("***** R9.InterfejsNiestandardowy *****\n");

            // Tworzona tablica obiektów kompatybilnych z Shape, typu Shape ...
            Shape[] myShapes = { new Hexagon(), new Circle(), new Triangle("Joe"), new Circle("JoJo")};

            // ... lecz nie ma gwarancji że każdy element tablicy implementuje Interfejs IPointy (tutaj klasa Circle nie implementuje)
            IPointy firstPointyItem = FindFirstPointyShape(myShapes);
            Console.WriteLine("Bezpieczne wywołanie składowej Interfejsu IPointy. The item has {0} points", firstPointyItem.Points); //tutaj bezpiecznie wykonuje się składowa Interfejsu - IPointy.Points!


            for (int i = 0; i < myShapes.Length; i++)
            {
                // Każdego obiektu tablicy Shape[], klasą bazową jest Shape, więc każdy musi implementować metodę Draw();
                myShapes[i].Draw();

                // zwraca true jeśli obiekt z tablicy implementuje Interfesj IPointy;
                // co pozwala na bezpieczne wywołać (po rzutowaniu).Points
                if (myShapes[i] is IPointy)
                    Console.WriteLine("-> Points: {0}", ((IPointy)myShapes[i]).Points);
                else
                    Console.WriteLine("-> {0}\'s not pointy!", myShapes[i].PetName);

                // Podobnie jak z IPointy - tutaj obiekt z tablicy rzutowany na IDraw3D;
                // dalej przekazany jest do statycznej metody która wykonuje składowe Interfejsu IDraw3D.Draw3D
                if (myShapes[i] is IDraw3D)
                    DrawIn3D((IDraw3D)myShapes[i]);

                Console.WriteLine();
            }

            // Tablica IPointy może przechowywać jedynie obiekty implementujące Interfejs IPointy
            // Jest możliwa iteracja po takiej tablicy niezależnie od różnorodności hierarchii klas
            IPointy[] myPointyObjects = {new Hexagon(), new Knife(),
                    new Triangle(), new Fork(), new PitchFork()};

            foreach (IPointy i in myPointyObjects)
                Console.WriteLine("Object has {0} points.", i.Points);

            Console.ReadLine();
        }

        #region Statyczna metoda wywołuje składową Draw3D każdego obiektu podanego w arg

        /// <summary>
        /// Interfejs jako argument metody - przyjmuje każdy obiekt implementujący Imterfejs IDraw3D
        /// </summary>
        /// <param name="itf3d"></param>
        static void DrawIn3D(IDraw3D itf3d)
        {
            Console.WriteLine("-> Drawing IDraw3D compatible type");
            itf3d.Draw3D();
        }

        #endregion

        #region Statyczna metoda zwraca pierwszy obiekt z tablicy implementujący IPointy

        /* Zakładając że tablica wejściowa ma 50 obiektów kompatybilnych z Shape, nie każdy musi implementować interfejs IPointy
         * dlatego w pesymistycznej sytuacji trzeba jakoś temu zaradzić(try/catch) lub zapobiec (IS/AS) */
        
        /// <summary>
        /// Pierwszy obiekt z podanej tablicy implementujący IPointy, zwraca jako Interfejs
        /// </summary>
        /// <param name="shapes"></param>
        /// <returns></returns>
        static IPointy FindFirstPointyShape(Shape[] shapes)
        {
            #region Jawne rzutowanie - kiepski wybór
            /* Kiepski sposób na sprawdzenie w RUNTIME, czy dany typ obsługuje konkretny Interfejs 
             * - blok try/catch i mieć nadzieje że nic złego się nie zdarzy */
            Circle c = new Circle();
            IPointy tmpIPointy = null;
            try
            {
                tmpIPointy = (IPointy)c;
                Console.WriteLine(tmpIPointy.Points);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region Uzyskiwanie referencji do Interfejsu - AS/IS

            // IS: jeśli sprawdzany obiekt nie implementuje podanego Interfejsu - zwracany False;
            // AS: jeśli obiekt można potraktować jak podany Interfejs zwracana jest referencja do tego interfejsu, jeśli nie - null;
            // jeśli typ jest kompatybilny z podanym Interfejsem, można bezpiecznie wywołać składowe, bez bloków try/catch
            foreach (Shape s in shapes)
            {
                if (s is IPointy)
                    return s as IPointy;
            }
            return null;
            
            #endregion
        }

        #endregion
    }


    #region Klasy implementujące Interfes
    // These types are just for testing purposes.
    // They illustrate how the same interface can be
    // supported in unique heirarchies.

    public class PitchFork : IPointy
    {
        public byte Points
        {
            get { return 3; }
        }
    }

    public class Fork : IPointy
    {
        public byte Points
        {
            get { return 4; }
        }
    }

    public class Knife : IPointy
    {
        public byte Points
        {
            get { return 1; }
        }
    }

    #endregion



    #region Shape base class
    // The abstract base class of the hierarchy.
    abstract class Shape
    {
        public Shape()
        { PetName = "NoName"; }

        public Shape(string name)
        { PetName = name; }

        // A single abstract method.
        public abstract void Draw();

        public string PetName { get; set; }
    }
    #endregion

    #region Circle class
    // If we did not implement the abstract Draw() method, Circle would also be
    // considered abstract, and would have to be marked abstract!
    class Circle : Shape
    {
        public Circle() { }
        public Circle(string name) : base(name) { }
        public override void Draw()
        {
            Console.WriteLine("Drawing {0} the Circle", PetName);
        }
    }
    #endregion

    #region Hexagon class
    // Hexagon DOES override Draw().
    // Hexagon now implements IPointy.
    class Hexagon : Shape, IPointy, IDraw3D
    {
        public Hexagon() { }
        public Hexagon(string name) : base(name) { }
        public override void Draw()
        { Console.WriteLine("Drawing {0} the Hexagon", PetName); }

        // IPointy Implementation.
        public byte Points
        {
            get { return 6; }
        }

        #region IDraw3D Members
        public void Draw3D()
        { Console.WriteLine("Drawing Hexagon in 3D!"); }
        #endregion
    }
    #endregion

    #region ThreeDCircle class
    // This class extends Circle and hides the inherited Draw() method.
    class ThreeDCircle : Circle, IDraw3D
    {
        // Hide any Draw() implementation above me.
        public new void Draw()
        {
            Console.WriteLine("Drawing a 3D Circle");
        }

        public new string PetName { get; set; }

        #region IDraw3D Members
        public void Draw3D()
        { Console.WriteLine("Drawing Circle in 3D!"); }
        #endregion
    }
    #endregion

    #region Triangle
    // New Shape derived class named Triangle.
    class Triangle : Shape, IPointy
    {
        public Triangle() { }
        public Triangle(string name) : base(name) { }
        public override void Draw()
        { Console.WriteLine("Drawing {0} the Triangle", PetName); }

        // IPointy Implementation.
        public byte Points
        {
            get { return 3; }
        }
    }
    #endregion
}
