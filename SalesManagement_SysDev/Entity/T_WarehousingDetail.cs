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
    class T_WarehousingDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("WaDetailID", TypeName = "INT", Order = 7)]
        [DisplayName("入庫詳細ID")]
        public int WaDetailID { get; set; }     //入庫詳細ID

        [Required]
        [Column("WaID", TypeName = "INT", Order = 0)]
        [DisplayName("入庫ID")]
        public int WaID { get; set; }           //入庫ID

        [Required]
        [Column("PrID", TypeName = "INT", Order = 8)]
        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID

        [Required]
        [Column("WaQuantity", TypeName = "INT", Order = 9)]
        [DisplayName("数量")]
        public int WaQuantity { get; set; }     //数量

    }
}
