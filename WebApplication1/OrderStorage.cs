


public class Order
{
    public int Count { get; set; }
    public DateTime Timestamp { get; set; }

    public Dictionary<string, int> Items { get; set; } = new(); // 餐點名稱與數量
}

public static class OrderStorage
{
    private static readonly Dictionary<string, List<Order>> _orders = new();

    public static void AddOrder(string restaurantId, Order order)
    {
        if (!_orders.ContainsKey(restaurantId))
        {
            _orders[restaurantId] = new List<Order>();
        }

        _orders[restaurantId].Add(order);
    }

    public static List<Order> GetOrders(string restaurantId)
    {
        return _orders.ContainsKey(restaurantId) ? _orders[restaurantId] : new List<Order>();
    }
}