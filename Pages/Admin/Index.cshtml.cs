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
        public int ToplamHaber { get; set; }
        public int YayindaHaber { get; set; }
        public int TaslakHaber { get; set; }
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Haber> Haberler { get; set; }
       
        public async Task OnGetAsync()
        {
            Haberler = await _context.Haberler.OrderByDescending(h => h.PublishedDate).ToListAsync();
            ToplamHaber = _context.Haberler.Count();
            YayindaHaber = _context.Haberler.Count(h => h.IsPublish);
            TaslakHaber = _context.Haberler.Count(h => !h.IsPublish);
        }

        public async Task<IActionResult> OnPostPublishAsync(int id)
        {
            var haber = await _context.Haberler.FindAsync(id);
            if (haber == null) return NotFound();

            haber.IsPublish = true;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        //Sil iþlemi
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var haber = await _context.Haberler.FindAsync(id);
            if (haber == null)
                return NotFound();

            _context.Haberler.Remove(haber);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
      
    }
}

