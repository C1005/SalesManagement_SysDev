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
    class T_ShipmentDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("ShDetailID", TypeName = "INT", Order = 9)]
        [DisplayName("出荷詳細ID")]
        public int ShDetailID { get; set; }     //出荷詳細ID    

        [Required]
        [Column("ShID", TypeName = "INT", Order = 0)]
        [DisplayName("出荷ID")]
        public int ShID { get; set; }           //出荷ID  

        [Required]
        [Column("PrID", TypeName = "INT", Order = 10)]
        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID  

        [Required]
        [Column("ShDquantity", TypeName = "INT", Order = 11)]
        [DisplayName("数量")]
        public int ShDquantity { get; set; }	//数量
    }
}
