using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagement_SysDev.Entity
{
    class T_HattyuDetailProvisional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("HaDetailID", TypeName = "INT", Order = 7)]
        [DisplayName("発注詳細ID")]
        public int HaDetailID { get; set; } //発注詳細ID

        [Required]
        [Column("HaID", TypeName = "INT", Order = 0)]
        [DisplayName("発注ID")]
        public int HaID { get; set; }       //発注ID

        [Required]
        [Column("PrID", TypeName = "INT", Order = 8)]
        [DisplayName("商品ID")]
        public int PrID { get; set; }       //商品ID

        [Required]
        [Column("HaQuantity", TypeName = "INT", Order = 9)]
        [DisplayName("数量")]
        public int HaQuantity { get; set; }	//数量
    }
}
