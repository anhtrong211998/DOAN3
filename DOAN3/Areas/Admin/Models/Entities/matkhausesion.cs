using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOAN3.Areas.Admin.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Matkhau")]
    public partial class matkhausesion
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string tentaikhoan { get; set; }
        public string matkhau { get; set; }
        public string nhomatkhau { get; set; }
    }
}