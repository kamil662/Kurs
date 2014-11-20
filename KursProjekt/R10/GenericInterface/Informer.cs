using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjekt.R10.GenericInterface
{
    #region Interface

    // Interface Contravariant
    interface ISetInfo<in T>
    {
        void SetInfo(T data);
    }

    // Interface Covariant 
    interface IGetInfo<out T>
    {
        T GetInfo();
    }

    #endregion

    /// <summary>
    /// Klasa Informer od T przechowuje jedną informację o nieokreślonym typie. 
    /// Interfejst definiują metody, które klasa Informer implementuje. 
    /// Służą one do obsługiwania informacji - pobierania i zapisywania.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Informer<T> : ISetInfo<T>, IGetInfo<T>
    {
        // Zmienna przechowująca informacje
        private T info;


        void ISetInfo<T>.SetInfo(T data)
        {
            this.info = data;
        }

        T IGetInfo<T>.GetInfo()
        {
            return this.info;
        }
    }
}
