


public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid(); // ← ✅ 加這個欄位
    public int Count { get; set; }
    public DateTime Timestamp { get; set; }
    public Dictionary<string, int> Items { get; set; } = new();
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

        Console.WriteLine($"新增訂單 ID: {order.Id}");
    }

    public static void RemoveOrder(string restaurantId, Guid id)
    {
        if (_orders.TryGetValue(restaurantId, out var list))
        {
            Console.WriteLine($"目前清單數量: {list.Count}");
            var order = list.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                Console.WriteLine($"找到並移除訂單 ID: {id}");
                list.Remove(order);
            }
            else
            {
                Console.WriteLine($"沒找到 ID: {id} 的訂單");
            }
        }
    }

    public static List<Order> GetOrders(string restaurantId)
    {
        return _orders.ContainsKey(restaurantId) ? _orders[restaurantId] : new List<Order>();
    }
}