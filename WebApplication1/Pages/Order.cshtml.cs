using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderModel(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [BindProperty(SupportsGet = true)]
        public string RestaurantId { get; set; } = "";

        [BindProperty]
        public int Counter { get; set; }

        public bool Submitted { get; set; } = false;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Submitted = true;

            OrderStorage.AddOrder(RestaurantId, new Order
            {
                Count = Counter,
                Timestamp = DateTime.Now
            });

            await _hubContext.Clients.All.SendAsync($"NewOrder_{RestaurantId}");

            return Page();
        }
    }
}
