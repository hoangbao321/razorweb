using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cs58_Razor_09.Models
{
    public class Article
    {
        [Key]
        public int ID { get; set; }

        [StringLength(255)]
        [Required(AllowEmptyStrings = false)]
        [Column(TypeName = "nvarchar")]
        public string Titile { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Created { get; set; } // ngày tạo ra bài viết

        [Column(TypeName = "ntext")]
        public string Content { get; set; }
    }
}
