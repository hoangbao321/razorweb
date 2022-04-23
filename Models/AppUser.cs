using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cs58_Razor_09.Models
{
    public class AppUser : IdentityUser
    {
        // khi nào thêm properties thì cho thêm giờ chưa cần
        [Column(TypeName = "nvarchar")]
        [StringLength(400,MinimumLength = 5)]
        public string HomeAddress{ get; set; }

    }
}
