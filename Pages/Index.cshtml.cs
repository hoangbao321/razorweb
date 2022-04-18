using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cs58_Razor_09.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace cs58_Razor_09.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly MyBlogContext myBlogContext;
        public IndexModel(ILogger<IndexModel> logger, MyBlogContext _myBlogContext)
        {
            _logger = logger;
            myBlogContext = _myBlogContext;
        }

        public void OnGet()
        {
            var posts = (from p in myBlogContext.articles
                        orderby p.Created descending
                        select p).ToList();
            ViewData["posts"] = posts;
        }
    }
}
