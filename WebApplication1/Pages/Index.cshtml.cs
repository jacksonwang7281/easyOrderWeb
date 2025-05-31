using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IHubContext<OrderHub> _hubContext;

        public IndexModel(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [BindProperty]
        public int Counter { get; set; }

        public bool Submitted { get; set; } = false;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Submitted = true;

            OrderStorage.Orders.Add(new Order
            {
                Count = Counter,
                Timestamp = DateTime.Now
            });

            // �q���Ҧ��s�u���Ȥ�ݡ]�p�С^���s�q��
            await _hubContext.Clients.All.SendAsync("NewOrder");

            return Page();
        }
    }
}
