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
    class T_ChumonDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("ChDatailID", TypeName = "INT", Order = 9)]
        [DisplayName("注文詳細ID")]
        public int ChDetailID { get; set; }     //注文詳細ID

        [Required]
        [Column("ChID", TypeName = "INT", Order = 0)]
        [DisplayName("注文ID")]
        public int ChID { get; set; }           //注文ID

        [Required]
        [Column("PrID", TypeName = "INT", Order = 10)]
        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID

        [Required]
        [Column("ChQuantity", TypeName = "INT", Order = 11)]
        [DisplayName("数量")]
        public int ChQuantity { get; set; }	    //数量
    }
}
