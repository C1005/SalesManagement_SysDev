using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagement_SysDev.Entity
{
    class T_OrderDetailProvisional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("OrDetailID", TypeName = "INT", Order = 9)]
        [DisplayName("受注詳細ID")]
        public int OrDetailID { get; set; }     //受注詳細ID

        [Required]
        [Column("OrID", TypeName = "INT", Order = 0)]
        [DisplayName("受注ID")]
        public int OrID { get; set; }           //受注ID

        [Required]
        [Column("PrID", TypeName = "INT", Order = 10)]
        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID

        [Required]
        [Column("OrQuantity", TypeName = "INT", Order = 11)]
        [DisplayName("数量")]
        public int OrQuantity { get; set; }	    //数量

        [Required]
        [Column("OrTotalPrice", TypeName = "INT", Order = 12)]
        [DisplayName("合計金額")]
        public int OrTotalPrice { get; set; }	//合計金額
    }
}
