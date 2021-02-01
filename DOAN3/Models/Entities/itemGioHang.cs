using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOAN3.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("CTDHang")]
    public partial class itemGioHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
    }
}