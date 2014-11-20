using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Wykorzystanie generycznych interfejsów IEnumerable<T>/IEnumerator<T> do przechodzenia po strukturze drzewiastej
 * Example:
 * http://www.cezarywalenciuk.pl/post/2011/09/27/IEnumerable-i-IEnumerator-implementowanie-tych-interfejsow.aspx
 * 
 * Klasa implementuje IEnumerator<T> - klasa reprezentuje obiekt odpowiedzialny za wyliczenie elementów z kolekcji.
 * Kolekcją jest Queue<T> przechowująca dane zebrane z drzewa - Tree<T>
 * 
 * Przepisywaniem danych ze struktury drzewiastej do kolejki zajmuje się metoda PrzepiszDrzewoDoKolejki(), 
 * wykorzystująca alrowytm chodzenia po strukturze drzewiastej;
 * 
 * NAJWAŻNIEJSZYMI elementami klasy są implementacje interfesu - metody MoveNext(), właściwości Current;
 * - MoveNext() - jeśli kolejka jest pusta, wypełnia ją posługując się metodą PrzepiszDrzewoDoKolejki(), 
 *                jeśli kolejka nie jest pusta, i posiada co najmniej jeden element - jest on zdejmowany (Dequeue() )
 *                i zapisywany do prywatnego pola, udostępnianego przez Current;
 */


namespace KursProjekt.R10.GenericClass
{

    // Klasa generuje enumerator dla obiektu Tree(TItem)
    internal class TreeEnumerator<TItem> : IEnumerator<TItem> where TItem : IComparable<TItem>
    {
        private Tree<TItem> currentData = null;     // Referencje do drzewa binarnego które będzie enumerowane
        private TItem currentItem = default(TItem); // Wartość zwracaną przez właściwość Current.
        private Queue<TItem> enumData = null;       // Kolekcja/kolejka przechowująca dane zebrane z drzewa

        // Konstruktor
        public TreeEnumerator(Tree<TItem> item)
        {
            this.currentData = item;
        }


        public void PrzepiszDrzewoDoKolejki(Queue<TItem> kolejka, Tree<TItem> tree)
        {
            if (tree.LeftTree != null)
                PrzepiszDrzewoDoKolejki(kolejka, tree.LeftTree);

            kolejka.Enqueue(tree.NodeData);

            if (tree.RightTree != null)
                PrzepiszDrzewoDoKolejki(kolejka, tree.RightTree);
        }


        #region Implementacja interfejsów

        /// <summary>
        /// Właściwość tylko do odczytu, przechowuje aktualny obiekt wyliczony 
        /// </summary>
        TItem IEnumerator<TItem>.Current
        {
            get
            {
                if (this.currentItem == null)
                    throw new InvalidOperationException("currentItem == null");
                return this.currentItem;
            }
        }

        /// <summary>
        /// Wykonuje enumeracje - nie zwraca elementu, a odświeża wartość w Current
        /// </summary>
        /// <returns>true jeśli są jeszcze elementy w kolejce</returns>
        bool System.Collections.IEnumerator.MoveNext()
        {
            if (enumData == null)
            {
                this.enumData = new Queue<TItem>();
                PrzepiszDrzewoDoKolejki(enumData, currentData);
            }

            if (enumData.Count > 0)
            {
                this.currentItem = enumData.Dequeue();
                return true;
            }
            return false;
        }

        void IDisposable.Dispose()
        {
            //throw new NotImplementedException();
        }

        object System.Collections.IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

        void System.Collections.IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
