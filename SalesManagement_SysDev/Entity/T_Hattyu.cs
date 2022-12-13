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
    class T_Hattyu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("HaID", TypeName = "INT", Order = 0)]
        [DisplayName("発注ID")]
        public int HaID { get; set; }                   //発注ID	

        [Required]
        [Column("MaID", TypeName = "INT", Order = 1)]
        [DisplayName("メーカID")]
        public int MaID { get; set; }                   //メーカID	

        [Required]
        [Column("EmID", TypeName = "INT", Order = 2)]
        [DisplayName("発注社員ID")]
        public int EmID { get; set; }                   //発注社員ID

        [Required]
        [Column("HaDate", TypeName = "date", Order = 3)]
        [DisplayName("発注年月日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime HaDate { get; set; }            //発注年月日	

        [Column("WaWarehouseFlag", TypeName = "INT", Order = 4)]
        [DisplayName("入庫済フラグ（倉庫）")]
        public int? WaWarehouseFlag { get; set; }   //入庫済フラグ（倉庫）

        [Required]
        [Column("HaFlag", TypeName = "INT", Order = 5)]
        [DisplayName("発注管理フラグ")]
        public int HaFlag { get; set; }             //発注管理フラグ

        [Column("HaHidden", TypeName = "nvarchar", Order = 6)]
        [DisplayName("非表示理由")]
        public String HaHidden { get; set; }            //非表示理由
    }

    class T_HattyuDsp
    {
        [DisplayName("発注ID")]
        public int HaID { get; set; }                   //発注ID	

        [DisplayName("メーカID")]
        public int MaID { get; set; }                   //メーカID	

        [DisplayName("発注社員ID")]
        public int EmID { get; set; }                   //発注社員ID

        [DisplayName("発注年月日")]
        public DateTime HaDate { get; set; }            //発注年月日	

        [DisplayName("入庫済フラグ（倉庫）")]
        public int? WaWarehouseFlag { get; set; }   //入庫済フラグ（倉庫）

        [DisplayName("発注管理フラグ")]
        public int HaFlag { get; set; }             //発注管理フラグ

        [DisplayName("非表示理由")]
        public String HaHidden { get; set; }            //非表示理由

        [DisplayName("発注詳細ID")]
        public int HaDetailID { get; set; }        //発注詳細ID

        [DisplayName("商品ID")]
        public int PrID { get; set; }              //商品ID

        [DisplayName("数量")]
        public int HaQuantity { get; set; }        //数量
    }
}
