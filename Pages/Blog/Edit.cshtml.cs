using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cs58_Razor_09.Models;

namespace cs58_Razor_09.Pages_Blog
{
    public class EditModel : PageModel
    {
        private readonly cs58_Razor_09.Models.MyBlogContext _context;

        public EditModel(cs58_Razor_09.Models.MyBlogContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) // nếu ko dc thẩm định
            {
                return Page();
            }

            _context.Attach(Article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.ID))
                {
                    return Content("ko thay bai viết");
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool ArticleExists(int id)
        {
            return _context.articles.Any(e => e.ID == id);
        }
    }
}
