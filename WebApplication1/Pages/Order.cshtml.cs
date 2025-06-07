using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Pages
{

    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFile { get; set; }
    }


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
        public List<int> Quantities { get; set; } = new();

        public bool Submitted { get; set; } = false;

        public List<MenuItem> MenuItems { get; set; } = new()
    {
        new MenuItem { Id = 0, Name = "º~³ù", ImageFile = "burger.jpg" },
        new MenuItem { Id = 1, Name = "©ÜÂÄ", ImageFile = "pizza.jpg" },
        new MenuItem { Id = 2, Name = "¬µÂû", ImageFile = "fried_chicken.jpg" },
        new MenuItem { Id = 3, Name = "¥i¼Ö", ImageFile = "cola.jpg" },
        new MenuItem { Id = 4, Name = "¸q¤j§QÄÑ", ImageFile = "pasta.jpg" },
        new MenuItem { Id = 5, Name = "¨F©Ô", ImageFile = "salad.jpg" },
        new MenuItem { Id = 6, Name = "¤û±Æ", ImageFile = "steak.jpg" },
        new MenuItem { Id = 7, Name = "³J¿|", ImageFile = "cake.jpg" },
        new MenuItem { Id = 8, Name = "¦B²N²O", ImageFile = "icecream.jpg" },
        new MenuItem { Id = 9, Name = "´ö", ImageFile = "soup.jpg" }
    };

        public async Task<IActionResult> OnPostAsync()
        {
            Submitted = true;

            int total = Quantities.Sum();

            OrderStorage.AddOrder(RestaurantId, new Order
            {
                Count = total,
                Timestamp = DateTime.Now
            });

            await _hubContext.Clients.All.SendAsync($"NewOrder_{RestaurantId}");

            return Page();
        }
    }
}
