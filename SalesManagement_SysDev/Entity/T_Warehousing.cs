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
    class T_Warehousing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("WaID", TypeName = "INT", Order = 0)]
        [DisplayName("入庫ID")]
        public int WaID { get; set; }               //入庫ID	

        [Required]
        [Column("HaID", TypeName = "INT", Order = 1)]
        [DisplayName("発注ID")]
        public int HaID { get; set; }               //発注ID	

        [Required]
        [Column("EmID", TypeName = "INT", Order = 2)]
        [DisplayName("入庫確認社員ID")]
        public int EmID { get; set; }               //入庫確認社員ID

        [Required]
        [Column("WaDate", TypeName = "date", Order = 3)]
        [DisplayName("入庫年月日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime WaDate { get; set; }        //入庫年月日	

        [Column("WaShelfFlag", TypeName = "INT", Order = 4)]
        [DisplayName("入庫済フラグ(棚)")]
        public int? WaShelfFlag { get; set; }       //入庫済フラグ(棚)

        [Column("WaHidden", TypeName = "nvarchar", Order = 5)]
        [DisplayName("非表示理由")]
        public String WaHidden { get; set; }	    //非表示理由

        [Required]
        [Column("WaFlag", TypeName = "INT", Order = 6)]
        [DisplayName("入庫管理フラグ")]
        public int WaFlag { get; set; }	            //入庫管理フラグ
    }

    class T_WarehousingDsp
    {
        [DisplayName("入庫ID")]
        public int WaID { get; set; }               //入庫ID	

        [DisplayName("発注ID")]
        public int HaID { get; set; }               //発注ID	

        [DisplayName("入庫確認社員ID")]
        public int EmID { get; set; }               //入庫確認社員ID

        [DisplayName("入庫年月日")]
        public DateTime WaDate { get; set; }        //入庫年月日	

        [DisplayName("入庫済フラグ(棚)")]
        public int? WaShelfFlag { get; set; }       //入庫済フラグ(棚)

        [DisplayName("非表示理由")]
        public String WaHidden { get; set; }	    //非表示理由

        [DisplayName("入庫管理フラグ")]
        public int WaFlag { get; set; }	            //入庫管理フラグ

        [DisplayName("入庫詳細ID")]
        public int WaDetailID { get; set; }     //入庫詳細ID

        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID

        [DisplayName("数量")]
        public int WaQuantity { get; set; }     //数量
    }
}
