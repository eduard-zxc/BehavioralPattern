public interface IObserver
{
    string Name { get; set; }
    void Update(string message);
}

public class Order
{
    private List<IObserver> _observers = new List<IObserver>();
    public string OrderId { get; private set; }
    private string _status;

    public Order(string orderId)
    {
        OrderId = orderId;
        _status = "Created";
    }

    public void Subscribe(IObserver observer)
    {
        _observers.Add(observer);
        Console.WriteLine($"{observer.Name} subscribed to Order {OrderId}");
    }

    public void Unsubscribe(IObserver observer)
    {
        _observers.Remove(observer);
        Console.WriteLine($"{observer.Name} unsubscribed from Order {OrderId}");
    }

    public void ChangeStatus(string newStatus)
    {
        _status = newStatus;
        NotifyObservers();
    }

    private void NotifyObservers()
    {
        string message = $"Order {OrderId} status changed to {_status}";
        foreach (var observer in _observers)
        {
            observer.Update(message);
        }
    }
}

public class Customer : IObserver
{
    public string Name { get; set; }
    public string PreferredContactMethod { get; set; }

    public Customer(string name, string method)
    {
        Name = name;
        PreferredContactMethod = method;
    }

    public void Update(string message)
    {
        Console.WriteLine($"{PreferredContactMethod} to Customer {Name}: {message}");
    }
}

public class Staff : IObserver
{
    public string Name { get; set; }

    public Staff(string name)
    {
        Name = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"Notification to Staff {Name}: {message}");
    }
}

class Program
{
    static void Main()
    {
        var order = new Order("A123");

        var customer1 = new Customer("Alice", "Email");
        var customer2 = new Customer("Bob", "SMS");
        var staff1 = new Staff("Eve");

        order.Subscribe(customer1);
        order.Subscribe(customer2);
        order.Subscribe(staff1);

        order.ChangeStatus("Processing");
        order.ChangeStatus("Ready for Shipping");

        order.Unsubscribe(customer2);
        order.ChangeStatus("Shipped");

        Console.ReadLine();
    }
}
