


public class Order
{
    public int Count { get; set; }
    public DateTime Timestamp { get; set; }
}

public static class OrderStorage
{
    public static List<Order> Orders { get; } = new List<Order>();
}