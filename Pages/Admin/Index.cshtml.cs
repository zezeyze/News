using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewsPage.Data;
using NewsPage.Models;

namespace NewsPage.Pages.Admin
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
            Haberler = await _context.Haberler.OrderByDescending(h => h.PublishedDate).ToListAsync();
        }

        public async Task<IActionResult> OnPostPublishAsync(int id)
        {
            var haber = await _context.Haberler.FindAsync(id);
            if (haber == null) return NotFound();

            haber.IsPublish = true;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
