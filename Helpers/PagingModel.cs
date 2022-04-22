using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cs58_Razor_09.Helpers
{
    public class PagingModel
    {
        public int currentPage { get; set; }
        public int countPages { get; set; }
        public Func<int?,string,DateTime?, DateTime?,string> generateUrl { get; set; }
        // chuỗi tìm kiếm
        public string Search { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
    }
}
