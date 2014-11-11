using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjekt.R9
{
    /*Example:
     * http://msdn.microsoft.com/pl-pl/library/234b841s(v=vs.110).aspx
     * 
     * Parametr dla metody ComapreTo musi być zgodny z typem określonym w interfejsie IComparable<T>
     */
    public class Cuboid : System.IComparable<Cuboid>
    {
        private int height;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        private int length;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        

        public Cuboid(int Height, int Length, int Width)
        {
            this.Height = Height;
            this.Length = Length;
            this.Width = Width;
        }

        public int Volume()
        {
            return Height * Length * Width;
        }

        public int CompareTo(Cuboid other)
        {
            if (this.Volume() == other.Volume())
                return 0;
            if (this.Volume() > other.Volume())
                return 1;
            return -1;
        }

        public override string ToString()
        {
            return string.Format("Cuboid=> Height: {0}, Length: {1}, Width:{2}, Volume:{3}", Height, Length, Width, Volume());
        }
    }

    public class comparerLength : IComparer<Cuboid>
    {

        public int Compare(Cuboid x, Cuboid y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null,
                    // y is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare 
                    //
                    return x.Length.CompareTo(y.Length);
                }
            }
        } //end CompareTo()
    }

}
