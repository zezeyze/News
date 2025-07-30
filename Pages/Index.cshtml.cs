using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewsPage.Data;
using NewsPage.Models;

namespace NewsPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Haber> Haberler { get; set; }

        public async Task OnGetAsync()
        {
            Haberler = await _context.Haberler
                                     .Where(h => h.IsPublish)
                                     .OrderByDescending(h => h.PublishedDate)
                                     .ToListAsync();
        }
    }
}
