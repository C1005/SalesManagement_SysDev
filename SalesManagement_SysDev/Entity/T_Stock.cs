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
    class T_Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("StID", TypeName = "INT", Order = 0)]
        [DisplayName("在庫ID")]
        public int StID { get; set; }           //在庫ID

        [Required]
        [Column("PrID", TypeName = "INT", Order = 1)]
        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID

        [Required]
        [Column("StQuantity", TypeName = "INT", Order = 2)]
        [DisplayName("在庫数")]
        public int StQuantity { get; set; }     //在庫数

        [Required]
        [Column("StFlag", TypeName = "INT", Order = 3)]
        [DisplayName("在庫管理フラグ")]
        public int StFlag { get; set; }	        //在庫管理フラグ

        [Column("StHidden", TypeName = "nvarchar", Order = 4)]
        [DisplayName("非表示理由")]
        public String StHidden { get; set; }            //非表示理由

    }
}
