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
    class T_SyukkoDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("SyDetailID", TypeName = "INT", Order = 9)]
        [DisplayName("出庫詳細ID")]
        public int SyDetailID { get; set; }     //出庫詳細ID

        [Required]
        [Column("SyID", TypeName = "INT", Order = 0)]
        [DisplayName("出庫ID")]
        public int SyID { get; set; }           //出庫ID

        [Required]
        [Column("PrID", TypeName = "INT", Order = 10)]
        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID

        [Required]
        [Column("SyQuantity", TypeName = "INT", Order = 11)]
        [DisplayName("数量")]
        public int SyQuantity { get; set; }	    //数量
    }
}
