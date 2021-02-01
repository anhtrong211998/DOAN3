using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOAN3.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("ThanhVienUser")]
    public partial class ThanhVienSession
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string Email { get; set; }
        public string matkhau { get; set; }
        public string Hoten { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
    }
}