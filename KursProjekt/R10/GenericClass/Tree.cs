using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Przykład użycia Generycznych klas realizujący algorytm drzewa binarnego przechowojącego dane dowolnego typu
 * gdzie typ danej musi implementować interfejs IComparable(), który ma definiować porównywanie obiektów.
 * 
 * Dla wbudowanych typów - int, string - nie ma problemu, 
 * ale dla własnych obiektów trzeba zdefiniować narzucony przez interfejs, metodę IComparable<T>.Compare()
 * 
 * Example:
 * http://www.cezarywalenciuk.pl/post/2011/08/28/Tworzenie-klasy-generycznej-i-drzewa-binarne.aspx
 * 
 * */

namespace KursProjekt.R10.GenericClass
{
    public class BinaryTreeGeneric
    {
        public void WorkMethod()
        {
            //tworzenie struktury danych TYPU int
            Tree<int> tree = new Tree<int>(5);//główny węzeł 
            tree.Insert(3);  tree.Insert(10);
            tree.Insert(1);  tree.Insert(4);
            tree.Insert(-1); tree.Insert(0);
            tree.Insert(4);  tree.Insert(7);
            tree.Insert(15); tree.Insert(16);
            tree.Insert(19); tree.Insert(8);
            tree.Insert(10); tree.Insert(9);
            tree.WalkTree();

            Console.ReadKey();
            /******************************************************************************************/


            //tworzenie struktury danych TYPU int
            Console.Clear();
            Tree<string> tree2 = new Tree<string>("Mortal Kombat");
            tree2.Insert("Defender Of Crown"); tree2.Insert("Gobliins");
            tree2.Insert("Settlers"); tree2.Insert("Dune 2");
            tree2.Insert("Mentor"); tree2.Insert("Franko");
            tree2.Insert("Doman"); tree2.Insert("Legion");
            tree2.Insert("Misja Harolda"); tree2.Insert("Eksperyment Deflin");
            tree2.Insert("Mortal Kombat 2"); tree2.Insert("Shadow of the Beast");
            tree2.Insert("Lost Patrol"); tree2.Insert("Monkey Island");
            tree2.Insert("Pinbal Fantasies"); tree2.Insert("Genghis Khan");
            tree2.Insert("The Lost Vikings"); tree2.Insert("Master Blaster");
            tree2.Insert("Ciemna Strona"); tree2.Insert("Another World");
            tree2.Insert("Flashback"); tree2.Insert("Lemmings");
            tree2.Insert("Blitz Bommbers"); tree2.Insert("Deluxe Galaga");
            StringBuilder sb2 = new StringBuilder();
            string wynik3 = tree2.WalkTree(sb2, true);
            Console.Write(wynik3);
            Console.ReadKey();
            /******************************************************************************************/

            // Test statycznej metody
            Console.Clear();
            Tree<char> tree3 = new Tree<char>('N');
            TreeStaticMetods.InsertIntoTree<char> (tree3, 'Z', 'B', 'G', 'I', 'E', 'W');
            StringBuilder sb3 = new StringBuilder();
            Console.WriteLine(tree3.WalkTree(sb3, false));
            Console.ReadKey();
            /******************************************************************************************/

            //tworzenie struktury danych własnego TYPU - Cuboid
            Console.Clear();
            Tree<Cuboid> tree4 = new Tree<Cuboid>(new Cuboid(4, 5, 6));
            tree4.Insert(new Cuboid(1, 2, 3));
            tree4.Insert(new Cuboid(10, 20, 30));
            tree4.Insert(new Cuboid(12, 1, 9));
            StringBuilder sb4 = new StringBuilder();
            string wynik4 = tree2.WalkTree(sb4, true);
            Console.Write(wynik4);
            Console.ReadKey();
            /******************************************************************************************/


            Console.Clear();
            Tree<double> tree5 = new Tree<double>(double.NaN);
            tree5.Insert(3.14);
            tree5.Insert(5.555);
            tree5.Insert(0.1);
            tree5.Insert(-12.12);
            tree5.Insert(15.15);
            tree5.Insert(0);
            tree5.Insert(double.PositiveInfinity);
            tree5.Insert(double.NegativeInfinity);
            tree5.Insert(double.MaxValue);
            tree5.Insert(double.MinValue);
            tree5.Insert(-8.16);

            // Testowanie drzewa binarnego implementującego IEnumerable/IEnumerator
            foreach (double item in tree5)
            {
                Console.WriteLine(" {0} ", item);
            }
        }
    }

    #region Tree Structure

    /// <summary>
    /// Klasa Tree realizuje algortm drzewa binarnego
    /// Dodawanie kolejnych elementów wykonywane jest w Insert()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Tree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public T NodeData { get; set; } //Węzeł przechowuje dane
        public Tree<T> LeftTree { get; set; }
        public Tree<T> RightTree { get; set; }

        public Tree(T nodeValue)
        {
            this.NodeData = nodeValue;
            this.LeftTree = null;
            this.RightTree = null;
        }

        public void Insert(T newItem)
        {
            T currentValue = this.NodeData;
            // Jeśli obecna wartość w węźle jest większa od nowej
            // nową wartość dodaj do Lewego poddrzewa
            if (currentValue.CompareTo(newItem) > 0)
            {
                if (this.LeftTree == null)
                    this.LeftTree = new Tree<T>(newItem);
                else
                    this.LeftTree.Insert(newItem);
            }
            else
            {
                if (this.RightTree == null)
                {
                    this.RightTree = new Tree<T>(newItem);
                }
                else
                {
                    this.RightTree.Insert(newItem);
                }
            }
        }

        // Algorytm przechodzący przez Drzewo i wyświetlający wartości z NodeData
        // Czytanie od lewej do prawej <=> od wartości Min. do Max.
        public void WalkTree()
        {
            if (this.LeftTree != null)
            {
                this.LeftTree.WalkTree();
            }

            Console.WriteLine(this.NodeData.ToString());

            if (this.RightTree != null)
            {
                this.RightTree.WalkTree();
            }
        }

        // Flaga bool określa czy wyświetlane dane mają być w jednej linii, czy każdy węzeł wyświetlać w nowej
        public string WalkTree(StringBuilder sb, bool NewLineMode)
        {
            if (this.LeftTree != null)
            {
                this.LeftTree.WalkTree(sb, NewLineMode);
            }

            if (NewLineMode == false)
                sb.Append(string.Format("{0} , ", this.NodeData.ToString()));
            else sb.AppendLine(this.NodeData.ToString());

            if (this.RightTree != null)
            {
                this.RightTree.WalkTree(sb, NewLineMode);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Implementacja metody interfejsu IEnumerable,
        /// zwraca obiekt wyliczeniowy 
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new TreeEnumerator<T>(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    /******************************************************************************************/

    //Statyczna Metoda w statycznej klasie,
    static class TreeStaticMetods
    {
        // W arg jest instancja drzewa, 
        // oraz tablica argumentów typu T
        public static void InsertIntoTree<T>(Tree<T> tree, params T[] data)
        where T : IComparable<T>
        {
            if (data.Length == 0)
                throw new ArgumentException("BinaryTree:Tree:" + "InsertIntoTree: Must provide at least one data value");

            foreach (T item in data)
            {
                tree.Insert(item);
            }
        }
    }

    #endregion



    #region Cuboid implementujący IComparable<Cuboid>

    public class Cuboid : IComparable<Cuboid>
    {

        public int CompareTo(Cuboid other)
        {
            if (this.Volume() == other.Volume())
                return 0;
            if (this.Volume() > other.Volume())
                return 1;

            return -1;
        }

        public Cuboid(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public int Volume()
        {
            return a * b * c;
        }

        private int a;
        private int b;
        private int c;
    }

    #endregion
}
