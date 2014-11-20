using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Przypisanie metody do delegaty obiektu Car jest możliwe dzięki funkcji rejestrującej;
 * Można też metodę przypisać do typu delegata, dzięki temu że jest on zdefiniowany jako public.
 * 
 * Kowariancja delegaty daje możliwość zwracania przez przypisane metody obiekty które dziedziczą po sobie.
 * W tym przykładzie delegata "DelegataPojazdu" przyjmuje metodę zwracającą obiekt Car, 
 * oraz kolejną metodę zwracającą obiekt SportsCar jako typ potomny obiektu Car.
 * Dzięki kowariancji wystarczy w takim przypadku rzutować na wybrany obiekt (tu SportsCar).
 * */

namespace KursProjekt.R11.Delegate
{

    class CarDelegate
    {
        public void WorkMethod()
        {
            Console.WriteLine("***** Delegates as event enablers *****\n");

            // First, make a Car object.
            Car c1 = new Car("SlugBug", 100, 10);

            // Rejestrowanie metody podając tylko jej nazwę
            c1.RegisterWithCarEngine(CallMeHere);

            // rejestracja metody => przypisanie metody do delegaty obiektu Car
            // Tworzony jest nowy obiekt delegaty, w konstruktorze dostaje nazwę metody 
            c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));

            // Tym razem zatrzymywany jest obiekt deledat (zachowany jest wskaźnik), 
            // aby móc później wyrejestrować metodę
            Car.CarEngineHandler handler2 = new Car.CarEngineHandler(OnCarEngineEvent2);
            c1.RegisterWithCarEngine(handler2);

            // Speed up (this will trigger the events).
            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
                c1.Accelerate(20);

            // Unregister from the second handler. 
            c1.UnRegisterWithCarEngine(handler2);

            // We won't see the 'upper case' message anymore!
            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
                c1.Accelerate(20);
            Console.ReadLine();

            /****      Delegate Kowariancja   ***********************************/

            Console.WriteLine("***** Delegate Kowariancja *****\n");
            DelegataPojazdu targetA = new DelegataPojazdu(GetBasicCar);
            Car c = targetA();
            Console.WriteLine("Obtained a {0}", c);

            // Covariance allows this target assignment.
            DelegataPojazdu targetB = new DelegataPojazdu(GetSportsCar);
            SportsCar sc = (SportsCar)targetB();
            Console.WriteLine("Obtained a {0}", sc);
            Console.ReadLine();
        }

        #region Targets for the delegate.
        // Metody przypisane do deletata - one będą wywoływane gdy zostanie 
        // wywołany delegata
        public static void OnCarEngineEvent(string msg)
        {
            Console.WriteLine("\n***** Message From Car Object *****");
            Console.WriteLine("=> {0}", msg);
            Console.WriteLine("***********************************\n");
        }
        public static void OnCarEngineEvent2(string msg)
        {
            Console.WriteLine("=> {0}", msg.ToUpper());
        }
        static void CallMeHere(string msg)
        {
            Console.WriteLine("=> Message from Car: {0}", msg);
        }
        #endregion


        #region Kowariancja delegatów

        // Definicja delegaty który może zwracać 
        // obiet Car, oraz dziedziczący po nim SportsCar
        public delegate Car DelegataPojazdu();

        public static Car GetBasicCar()
        { 
            return new Car(); 
        }

        public static SportsCar GetSportsCar()
        { 
            return new SportsCar(); 
        }

        #endregion
    }

    public class Car
    {
        #region Basic Car state data / constructors

        // Internal state data.
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }

        // Is the car alive or dead?
        private bool carIsDead;

        public Car()
        {
            MaxSpeed = 100;
        }

        public Car(string name, int maxSp, int currSp)
        {
            CurrentSpeed = currSp;
            MaxSpeed = maxSp;
            PetName = name;
        }
        #endregion

        #region Delegate infrastructure
        // Definicja Delegata   
        public delegate void CarEngineHandler(string msgForCaller);

        // definicja zmiennej składowej tego delegata
        private CarEngineHandler listOfHandlers;

        // Funkcja rejestracyjna dla procedury wywołującej
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        { listOfHandlers += methodToCall; }

        public void UnRegisterWithCarEngine(CarEngineHandler methodToCall)
        { listOfHandlers -= methodToCall; }
        #endregion

        #region Accelerate method
        // metoda której celem jest umożliwienie obiektowi (Car) wysłania komunikatów
        // o stanie silnika do każdej zainteresowanej procedury.
        public void Accelerate(int delta)
        {
            // jeśli Car padł, wyslij komunikat za pomocą delegata
            if (carIsDead)
            {
                // Anybody listening?
                if (listOfHandlers != null)
                    listOfHandlers("Sorry, this car is dead...");
            }
            else
            {
                CurrentSpeed += delta;

                // Inny komunikat
                if (10 == (MaxSpeed - CurrentSpeed)
                  && listOfHandlers != null)
                {
                    listOfHandlers("Careful buddy!  Gonna blow!");
                }

                if (CurrentSpeed >= MaxSpeed)
                    carIsDead = true;
                else
                    Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
            }
        }
        #endregion
    }



    // No need to add any members here, 
    // just need a derived class to test.
    public class SportsCar : Car
    { }

}
