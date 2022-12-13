using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagement_SysDev
{
    class T_ArrivalDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("ArDetailID", TypeName = "INT", Order = 9)]
        [DisplayName("入荷詳細ID")]
        public int ArDetailID { get; set; }     //入荷詳細ID

        [Required]
        [Column("ArID", TypeName = "INT", Order = 0)]
        [DisplayName("入荷ID")]
        public int ArID { get; set; }           //入荷ID

        [Required]
        [Column("PrID", TypeName = "INT", Order = 10)]
        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID

        [Required]
        [Column("ArQuantity", TypeName = "INT", Order = 11)]
        [DisplayName("数量")]
        public int ArQuantity { get; set; }	    //数量
    }
}
