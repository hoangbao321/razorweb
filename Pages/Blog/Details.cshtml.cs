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
    public class DetailsModel : PageModel
    {
        private readonly cs58_Razor_09.Models.MyBlogContext _context;

        public DetailsModel(cs58_Razor_09.Models.MyBlogContext context)
        {
            _context = context;
        }

        public Article baobao { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return Content("ko thay bai viết");
            }

            baobao = await _context.articles.FirstOrDefaultAsync(m => m.ID == id);

            if (baobao == null)
            {
                return Content("ko thay bai viết");
            }
            return Page();
        }
    }
}
