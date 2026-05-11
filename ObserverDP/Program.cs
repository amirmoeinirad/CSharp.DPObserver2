
// Amir Moeini Rad
// September 2025

// The Observer Design Pattern
// Version: 2.0 -> The notification process is implemented using the C#'s built-in event system.

// This pattern defines a one-to-many dependency.
// An object, known as the subject, maintains a list of its dependents, called observers,
// and notifies them automatically of any state changes.
// This pattern is particularly useful in scenarios where a change in one object needs to be reflected in multiple other objects.


namespace ObserverDP
{    
    // The Subject Class    
    public class Stock
    {
        // Delegate for the event
        public delegate void PriceChangedHandler(int price);

        // Event to notify observers
        public event PriceChangedHandler? PriceChanged;

        // State of the subject (Stock Price)
        private int price;

        // Property to get/set the stock price
        public int Price
        {
            get => price;
            set
            {
                price = value;

                // Raise the event to notify all observers.
                PriceChanged?.Invoke(price);
            }
        }
    }
    


    // Observer interface
    public interface IObserver
    {
        void OnPriceChanged(int newPrice);
    }



    // Concrete Observer
    public class Investor : IObserver
    {       
        private string name;
        
        public Investor(string name) => this.name = name;

        // Update method to be called when the subject's state changes.
        public void OnPriceChanged(int newPrice)
        {
            Console.WriteLine($"{name} notified: Stock price changed to {newPrice}.");
        }
    }



    // Main Program
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("The Observer Design Pattern in C#.NET.");
            Console.WriteLine("--------------------------------------\n");


            // Create a stock object. (The Subject)
            Stock stock = new();

            // Create investors (Observers).
            Investor amir = new("Amir");
            Investor elham = new("Elham");

            // Subscribe to the event
            stock.PriceChanged += amir.OnPriceChanged;
            stock.PriceChanged += elham.OnPriceChanged;

            // Change the stock price and see how observers are notified.
            stock.Price = 100;
            Console.WriteLine();
            stock.Price = 120;
            Console.WriteLine();
            stock.Price = 110;
            Console.WriteLine();


            // Only Elham (investor b) will be notified this time.
            stock.PriceChanged -= amir.OnPriceChanged;
            stock.Price = 90;

            Console.WriteLine("\nDone.");
        }
    }
}
