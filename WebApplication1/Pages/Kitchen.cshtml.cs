using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    [IgnoreAntiforgeryToken] // ✅ 加這行避免 400 錯誤
    public class KitchenModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string RestaurantId { get; set; }

        public List<Order> Orders { get; set; } = new();

        public async Task<IActionResult> OnPostRemoveAsync([FromBody] Guid id)
        {
            var restaurantId = RouteData.Values["restaurantId"]?.ToString();
            if (string.IsNullOrEmpty(restaurantId))
            {
                Console.WriteLine("❌ restaurantId 是空的");
                return new JsonResult(new { success = false });
            }

            Console.WriteLine($"✅ 收到刪除 ID: {id}, restaurant: {restaurantId}");

            OrderStorage.RemoveOrder(restaurantId, id);
            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnPostRemoveTableAsync([FromBody] string tableNumber)
        {
            var restaurantId = RouteData.Values["restaurantId"]?.ToString();
            if (string.IsNullOrEmpty(restaurantId) || string.IsNullOrEmpty(tableNumber))
                return new JsonResult(new { success = false });

            Console.WriteLine($"🔴 移除整桌訂單：{tableNumber}");
            OrderStorage.RemoveOrdersByTable(restaurantId, tableNumber);
            return new JsonResult(new { success = true });
        }

        public void OnGet()
        {
            Orders = OrderStorage.GetOrders(RestaurantId);
            foreach (var order in Orders)
            {
                order.Items ??= new Dictionary<string, int>();
            }
        }
    }
}