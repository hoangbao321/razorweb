using cs58_Razor_09.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cs58_Razor_09.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly cs58_Razor_09.Models.MyBlogContext _context;
        public IList<Article> Article { get; set; }

        public const int ITEMS_PER_PAGE = 10;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int CurrentPage { get; set; }
        public int CountPage { get; set; }
        [BindProperty(SupportsGet = true, Name = "Search")]
        public string Search { get; set; }

        [BindProperty(SupportsGet = true, Name = "StartDay")]
        public DateTime? StartDay { get; set; }
        [BindProperty(SupportsGet = true, Name = "EndDay")]
        public DateTime? EndDay { get; set; }
        public IndexModel(cs58_Razor_09.Models.MyBlogContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string Search, DateTime? StartDay, DateTime? EndDay)
        {
            Article = await (from p in _context.articles
                             orderby p.ID descending
                             select p).ToListAsync();

            this.Search = Search;
            this.StartDay = (StartDay != null) ? StartDay : null;
            this.EndDay = (EndDay != null) ? EndDay : null;

            int total = 0;
            var skip = 0;
            CountPage = Article.Count();

            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            if (CurrentPage > CountPage)
            {
                CurrentPage = CountPage;
            }

            if (!string.IsNullOrEmpty(Search))
            {
                Article =await _context.articles.Where(p => p.Titile.Contains(Search)).ToListAsync();
                total  = Article.Count();
                CountPage = (int)(total / ITEMS_PER_PAGE) + 1;
                Article = await (from p in _context.articles
                                 orderby p.ID descending
                                 select p).Skip((CurrentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToListAsync();
            }

            if (StartDay != null && EndDay != null)
            {
                Article = _context.articles.Where(p => p.Created.Date >= StartDay.Value.Date && p.Created.Date <= EndDay.Value.Date).ToList();
                total = Article.Count();
                CountPage = (int)(total / ITEMS_PER_PAGE) + 1;
                skip = (CurrentPage - 1) * ITEMS_PER_PAGE;
                Article = Article.Skip(skip).Take(ITEMS_PER_PAGE).ToList();
            }


            if (string.IsNullOrEmpty(Search) && StartDay == null && EndDay == null)
            {
                total = _context.articles.Count();
                CountPage = (int)(total / ITEMS_PER_PAGE) + 1;
                Article = await _context.articles.Skip(ITEMS_PER_PAGE * (CurrentPage - 1)).Take(ITEMS_PER_PAGE).ToListAsync();
            }
        }
    }
}
