using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using cs58_Razor_09.Models;

namespace cs58_Razor_09.Pages_Blog
{
    public class DeleteModel : PageModel
    {
        private readonly cs58_Razor_09.Models.MyBlogContext _context;

        public DeleteModel(cs58_Razor_09.Models.MyBlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return Content("ko thay bai viết"); 
            }

            Article = await _context.articles.FirstOrDefaultAsync(m => m.ID == id);

            if (Article == null)
            {
                return Content("ko thay bai viết");

            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return Content("ko thay bai viết");
            }

            //Article = await _context.articles.FindAsync(id);

            Article = (from p in _context.articles
                       where p.ID == id
                       select p).FirstOrDefault();

            if (Article != null)
            {
                _context.articles.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
