using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [StringLength(255 ,MinimumLength =5 ,ErrorMessage ="{0} phải dài từ {2} đến {1}")]
        [Required(AllowEmptyStrings = false)]
        [Column(TypeName = "nvarchar")]
        [DisplayName("Tiêu Đề")]
        public string Titile { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Phải nhập {0}")]
        [DisplayName("Ngày Tạo")]
        public DateTime Created { get; set; } // ngày tạo ra bài viết

        [Column(TypeName = "ntext")]
        [DisplayName("Nội Dung")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Phải nhập {0}")]
        public string Content { get; set; }
    }
}
