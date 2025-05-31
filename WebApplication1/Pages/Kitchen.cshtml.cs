using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace WebApplication1.Pages
{
    public class KitchenModel : PageModel
    {
        public List<Order> Orders => OrderStorage.Orders;

        public void OnGet()
        {
            // �������J�ɷ|�q OrderStorage ����q��M��
        }
    }
}
