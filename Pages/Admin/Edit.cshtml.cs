using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsPage.Data;
using NewsPage.Models;

namespace NewsPage.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Haber Haber { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Haber = await _context.Haberler.FindAsync(id);
            if (Haber == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var haberInDb = await _context.Haberler.FindAsync(Haber.Id);
            if (haberInDb == null)
                return NotFound();

            haberInDb.Title = Haber.Title;
            haberInDb.Summary = Haber.Summary;
            haberInDb.IsPublish = Haber.IsPublish;

            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }

}