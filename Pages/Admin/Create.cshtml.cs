using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsPage.Data;
using NewsPage.Models;

namespace NewsPage.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateModel(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public Haber YeniHaber { get; set; }

        [BindProperty]
        public IFormFile Resim { get; set; }

        public void OnGet()
        {
            // Sayfa GET isteðinde boþ çalýþabilir
        }

        public async Task<JsonResult> OnPostSaveAsync()
        {
            //if (!ModelState.IsValid)
              //  return new JsonResult(new { success = false, message = "Geçersiz veri." });

            if (Resim != null)
            {
                var klasorYolu = Path.Combine(_env.WebRootPath, "images");
                var dosyaAdi = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(Resim.FileName)}";
                var tamYol = Path.Combine(klasorYolu, dosyaAdi);

                using (var stream = new FileStream(tamYol, FileMode.Create))
                {
                    await Resim.CopyToAsync(stream);
                }

                YeniHaber.ImagePath = "/images/" + dosyaAdi;
            }

            YeniHaber.PublishedDate = DateTime.Now;
            YeniHaber.IsPublish = false; 

            _context.Haberler.Add(YeniHaber);
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }
    }
}
