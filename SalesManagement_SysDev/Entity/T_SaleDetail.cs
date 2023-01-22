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
    class T_SaleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("SaDetailID", TypeName = "INT", Order = 8)]
        [DisplayName("売上明細ID")]
        public int SaDetailID { get; set; }         //売上明細ID

        [Required]
        [Column("SaID", TypeName = "INT", Order = 0)]
        [DisplayName("売上ID")]
        public int SaID { get; set; }               //売上ID

        [Required]
        [Column("PrID", TypeName = "INT", Order = 9)]
        [DisplayName("商品ID")]
        public int PrID { get; set; }               //商品ID

        [Required]
        [Column("SaQuantity", TypeName = "INT", Order = 10)]
        [DisplayName("個数")]
        public int SaQuantity { get; set; }         //個数

        [Required]
        [Column("SaPrTotalPrice", TypeName = "INT", Order = 11)]
        [DisplayName("合計金額")]
        public int SaPrTotalPrice { get; set; }	    //合計金額

    }
}
