using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class KitchenModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string RestaurantId { get; set; }

        public List<Order> Orders { get; set; }

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